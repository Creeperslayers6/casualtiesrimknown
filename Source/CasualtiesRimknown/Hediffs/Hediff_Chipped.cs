using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CasualtiesRimknown.Hediffs
{
    /*
     * DESIGN NOTES:
     * 
     * BRAIN CHIP EXPLOSION:
     * ONCE ARMED, BOMB EXPLODES IN 4.2 SECONDS.
     * ATTEMPT TO DESTROY THE BRAIN/HEAD INCLUDING EXPLODING THE SURROUNDINGS.
     * 
     * CHIP ARMING MECHANIC:
     * THE BOMB CHECKS TO TRIGGER THE ARMING COUNTDOWN ONCE THE PAWN IS DOWNED.
     * THE ARMING COUNTDOWN PICKS BETWEEN A RANGE FOR HOW LONG IT WILL COUNTDOWN BEFORE ATTEMPTING TO ARM THE BOMB.
     * IF THE PAWN IS STILL DOWNED OR HAS BECOME A PRISONER/SLAVE WHEN THE ARMING TIMER IS COMPLETE, THE BOMB WILL BE ARMED.
     * 
     * EMP STUN MECHANIC:
     * IN ORDER TO SAFELY RESCUE A RAIDER WITH THE BRAIN CHIP, THE PLAYER NEEDS TO HIT THE PAWN WITH AN EMP WEAPON.
     * ONCE THE EMP HITS, THE BRAIN CHIP IS STUNNED FOR AN X AMOUNT OF TIME.
     * THE DETONATION TIMER (ARMED) WILL BE PAUSED BY THE STUN.
     * THE ARMING TIMER, HOWEVER WILL NOT BE STUNNED BY THE EMP. INSTEAD, THE TIMER WILL BE HALTED EARLY BY X SECONDS SO THAT AMOUNT WILL BE LEFT ONCE THE STUN WEARS OFF.
     *      - (ARMING TIMER REPRESENTS INTERVENTION BY THE COMPANY, NOT AN ACTUAL TIMER IN THE CHIP)
    */ 
    public class Hediff_Chipped : HediffWithComps
    {
        // Explosion Stats
        private float explosionRadius = 2f;
        private int explosionDamage = 80;
        // States
        private bool explosiveArmed = false;
        private bool deviceArming = false;
        private bool stunnedByEMP = false;
        private bool stunnedBySolarFlare => pawn.Map.gameConditionManager.ElectricityDisabled(pawn.Map);

        private bool IsStunned => stunnedByEMP || stunnedBySolarFlare; //TODO: ADD TEXT INDICATION THAT CHIP IS STUNNED!
        // Timer Durations (seconds)
        private float detonationTimerDuration = 4.2f;
        private FloatRange armingTimerDurationRange = new FloatRange(5f, 300f); // Regular Arming Time After Activation (Simulates TheCompany™ Checking On Subject)
        private float minimumArmingTime = 5f; // Minimum Arming Time Left After Regular Arm Timer Finishes (Does not progress when stunned by EMP!)
        private float stunTimerDuration = 120f;
        // Timers
        TickTimer detonationTimer = new TickTimer();
        TickTimer armingTimer = new TickTimer();
        TickTimer armingTimerFinal = new TickTimer();
        TickTimer stunTimer = new TickTimer();
        //
        public override string LabelInBrackets
        {
            get
            {
                if (IsStunned)
                {
                    return "CR_NeuralChip_Stunned".Translate();
                }
                if (CurStage != null && !CurStage.label.NullOrEmpty())
                {
                    return CurStage.label;
                }
                return null;
            }
        }
        //
        private static readonly CachedTexture DetonateGizmoTexture = new CachedTexture("CasualtiesRimknown/UI/Gizmos/DetonateSkull");

        private void StartDetonationSequence()
        {
            explosiveArmed = true;
            detonationTimer.Start(GenTicks.TicksGame, detonationTimerDuration.SecondsToTicks(), Explode);
            DefOfs.SoundDefOf.CR_BrainChip_SelfDestruct.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
            pawn?.needs?.mood?.thoughts?.memories?.TryGainMemory(DefOfs.ThoughtDefOf.CR_BrainChip_SelfDestructing);
            Severity = 5;
        }

        private void StartArmingSequence()
        {
            deviceArming = true;
            armingTimer.Start(GenTicks.TicksGame, (armingTimerDurationRange.RandomInRange).SecondsToTicks(), StartArmingSequenceFinal);
        }
        private void StartArmingSequenceFinal()
        {
            armingTimerFinal.Start(GenTicks.TicksGame, minimumArmingTime.SecondsToTicks(), TryStartDetonationSequence);
        }
        private void StunDevice()
        {
            stunnedByEMP = true;
            stunTimer.Start(GenTicks.TicksGame, stunTimerDuration.SecondsToTicks(), OnStunCompleted);
        }

        private void OnStunCompleted()
        {
            stunnedByEMP = false;
        }

        private void TryStartDetonationSequence()
        {
            if (pawn.Downed || pawn.IsPrisoner || pawn.IsSlave)
            {
                StartDetonationSequence();
            }
            else
            {
                // Reset Device
                deviceArming = false;
            }
        }

        // Listens for EMP Damage
        public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.Notify_PawnPostApplyDamage(dinfo, totalDamageDealt);
            // Based on HediffComp.ReactOnDamage()
            if (dinfo.Def == DamageDefOf.EMP)
            {
                StunDevice();
            }
        }
        // Listens for Pawn being Downed!
        public override void Notify_Downed()
        {
            if (!deviceArming && pawn.Faction == Find.FactionManager.FirstFactionOfDef(DefOfs.FactionDefOf.CR_TheCompany))
            {
                StartArmingSequence();
            }
        }

        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            if (explosiveArmed && !detonationTimer.Finished && !IsStunned)
            {
                detonationTimer.TickIntervalDelta();
            }
            if (stunnedByEMP && !stunTimer.Finished)
            {
                stunTimer.TickIntervalDelta();
            }
            if (deviceArming && !armingTimer.Finished)
            {
                armingTimer.TickIntervalDelta();
            }
            if (armingTimer.Finished && !armingTimerFinal.Finished && !IsStunned)
            {
                armingTimerFinal.TickIntervalDelta();
            }
        }

        private void Explode()
        {
            Map currentMap = pawn.Map;
            pawn.TakeDamage(new DamageInfo(DamageDefOf.Bomb, 50, 0, -1, pawn, null, DefOfs.ThingDefOf.CR_BrainChipBomb));
            GenExplosion.DoExplosion(pawn.Position, currentMap, explosionRadius, DamageDefOf.Bomb, pawn, explosionDamage, -1, null, DefOfs.ThingDefOf.CR_BrainChipBomb);
            DestroyBrain();
        }

        private void DestroyBrain()
        {
            BodyPartRecord pawnBrain = pawn.health.hediffSet.GetBrain();
            if (pawnBrain != null)
            {
                if (pawn.Dead)
                {
                    BodyPartRecord pawnHead = pawn.health.hediffSet.GetBodyPartRecord(BodyPartDefOf.Head);
                    Hediff_MissingPart headExploded = (Hediff_MissingPart)HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, pawnHead);
                    headExploded.IsFresh = true;
                    pawn.health.AddHediff(headExploded);
                    Log.Message("Added Missing Head Hediff!");
                }
                else
                {
                    if (pawnBrain != null && !pawn.health.hediffSet.HasMissingPartFor(pawnBrain))
                    {
                        pawn.TakeDamage(new DamageInfo(DamageDefOf.Bomb, 999, 0, -1, pawn, pawnBrain, DefOfs.ThingDefOf.CR_BrainChipBomb));
                        Log.Message("Okay, we're just blowing this head up!");
                    }
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref explosiveArmed, "explosiveArmed", defaultValue: false);
            Scribe_Values.Look(ref deviceArming, "deviceArming", defaultValue: false);
            Scribe_Values.Look(ref stunnedByEMP, "stunnedByEMP", defaultValue: false);

            Scribe_Deep.Look(ref detonationTimer, "detonationTimer");
            Scribe_Deep.Look(ref armingTimer, "armingTimer");
            Scribe_Deep.Look(ref armingTimerFinal, "armingTimerFinal");
            Scribe_Deep.Look(ref stunTimer, "stunTimer");

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                detonationTimer.OnFinish = Explode;
                armingTimer.OnFinish = StartArmingSequenceFinal;
                armingTimerFinal.OnFinish = TryStartDetonationSequence;
                stunTimer.OnFinish = OnStunCompleted;
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
            if (!Visible && !DebugSettings.godMode)
            {
                yield break;
            }
            Command_Action commandIncreaseSeverity = new Command_Action();
            commandIncreaseSeverity.icon = DetonateGizmoTexture.Texture;
            commandIncreaseSeverity.defaultDesc = "CR_TerminateButton_Desc".Translate();
            switch (Severity)
            {
                case 1:
                    commandIncreaseSeverity.defaultLabel = "CR_TerminateButton_1".Translate();
                    break;
                case 2:
                    commandIncreaseSeverity.defaultLabel = "CR_TerminateButton_2".Translate();
                    commandIncreaseSeverity.SetColorOverride(new Color(0.702f, 0.56f, 0.545f));
                    break;
                case 3:
                    commandIncreaseSeverity.defaultLabel = "CR_TerminateButton_3".Translate();
                    commandIncreaseSeverity.SetColorOverride(new Color(0.722f, 0.376f, 0.337f));
                    break;
                case 4:
                    commandIncreaseSeverity.defaultLabel = "CR_TerminateButton_4".Translate();
                    commandIncreaseSeverity.SetColorOverride(new Color(0.745f, 0.067f, 0f));
                    break;
                case 5:
                    commandIncreaseSeverity.defaultLabel = "CR_TerminateButton_5".Translate();
                    commandIncreaseSeverity.SetColorOverride(new Color(0.745f, 0.067f, 0f));
                    break;
            }
            if (Severity == 5)
            {
                commandIncreaseSeverity.Disable("CR_TerminateButton_Disabled".Translate());
                commandIncreaseSeverity.defaultDesc = "CR_TerminateButton_Desc_Armed".Translate();
            }
            commandIncreaseSeverity.action = delegate
            {
                SoundInfo soundData = SoundInfo.OnCamera();
                soundData.pitchFactor = 1f - ((Severity - 1) * 0.15f);
                DefOfs.SoundDefOf.CR_BrainChip_GizmoSFX.PlayOneShot(soundData);
                Severity = Mathf.Clamp(Severity + 1, 0, 5);
                if (Severity == 5)
                {
                    StartDetonationSequence();
                }
            };
            yield return commandIncreaseSeverity;
        }
    }
}
