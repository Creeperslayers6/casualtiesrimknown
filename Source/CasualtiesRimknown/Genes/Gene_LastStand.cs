using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown
{
    public class Gene_LastStand : Gene
    {
        protected int usesLeft = 1;
        public int UsesLeft => usesLeft;

        private TickTimer resurrectTimer = new TickTimer();
        private TickTimer warmupTimer = new TickTimer();

        private bool resurrecting;
        public bool Resurrecting => resurrecting;
        private bool IsHollowed => pawn.health.hediffSet.HasHediff(DefOfs.HediffDefOf.CR_Hollow); // Hollowed Pawns can't Last Stand!
        private bool BrainIntact => pawn.health.hediffSet.GetPartHealth(pawn.health.hediffSet.GetBrain()) > 0; // Brain must be intact to trigger Last Stand!
        public bool CouldResurrect => usesLeft > 0 && !IsHollowed && BrainIntact;
        
        private float mentalBreakThreshold = 0.2f;

        private float lastKnownMood = 0.5f;
        protected float NormalizedMoodValue => (lastKnownMood - mentalBreakThreshold) / (1f - mentalBreakThreshold);

        private static readonly float ResurrectDurationSeconds = 8f;
        private static readonly FloatRange WarmupSeconds = new FloatRange(1f, 2f);

        private bool evaluatedChance = false;
        public bool EvaluateChance => evaluatedChance;

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            TryTriggerWarmupResurrection();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref usesLeft, "usesLeft", 0);
            Scribe_Values.Look(ref resurrecting, "resurrecting", defaultValue: false);
            Scribe_Deep.Look(ref resurrectTimer, "resurrectTimer");
            Scribe_Deep.Look(ref warmupTimer, "warmupTimer");
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                resurrectTimer.OnFinish = Resurrect;
                warmupTimer.OnFinish = Use;
                if (!resurrecting && pawn.Dead)
                {
                    TryTriggerWarmupResurrection();
                }
            }
        }

        private void TryTriggerWarmupResurrection()
        {
            //Log.Message("Gene_LastStand Trigger Wakeup!" + " : " + CouldResurrect);
            if (!resurrecting && CouldResurrect)
            {
                bool passedLastChanceCheck = SucceedLastStandRoll();
                //Log.Message("Roll Resurrect Chance: " + NormalizedMoodValue + " : " + passedLastChanceCheck);
                if (passedLastChanceCheck)
                {
                    resurrecting = true;
                    warmupTimer.Start(GenTicks.TicksGame, WarmupSeconds.RandomInRange.SecondsToTicks(), Use);
                }
            }
            evaluatedChance = true;
        }

        private void Use()
        {
            Messages.Message("CR_LastStandTriggered".Translate(pawn.Named("PAWN")), pawn, MessageTypeDefOf.NeutralEvent);
            resurrecting = true;
            usesLeft = Mathf.Max(usesLeft - 1, 0);
            resurrectTimer.Start(GenTicks.TicksGame, ResurrectDurationSeconds.SecondsToTicks(), Resurrect);
        }

        private void Resurrect()
        {
            //Log.Message("Gene_LastStand, Attempt Resurrect Now!");
            resurrecting = false;
            ResurrectionUtility.TryResurrect(pawn, new ResurrectionParams
            {
                gettingScarsChance = 0f,
                restoreMissingParts = false,
                canKidnap = false,
                canTimeoutOrFlee = false,
                useAvoidGridSmart = true,
                canSteal = false,
                invisibleStun = true
            });
            TaleRecorder.RecordTale(DefOfs.TaleDefOf.CR_LastStandTriggered, pawn);
            Find.BattleLog.Add(new BattleLogEntry_Event(pawn, DefOfs.RulePackDefOf.CR_Event_LastStandTriggered, pawn));

            // TEND BLEEDING WOUNDS!

            // FIX BLOOD LOSS

            // FEED PAWN?
            // RESET SLEEP?
            // HALF HEMOTHORAX!

            // AI PAWNS PICKUP CLOSEST WEAPON (DROPPED WHEN DOWNED)!
            if (pawn.Faction != Faction.OfPlayer && !pawn.Downed)
            {
                Thing thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.Weapon), PathEndMode.OnCell, TraverseParms.For(pawn), 5f);
                if (thing != null)
                {
                    Job job = JobGiver_PickupDroppedWeapon.PickupWeaponJob(pawn, thing, ignoreForbidden: true);
                    if (job != null)
                    {
                        pawn.jobs.StartJob(job, JobCondition.InterruptForced);
                    }
                }
            }
        }

        public void TickRare()
        {
            if (!warmupTimer.Finished)
            {
                warmupTimer.TickIntervalDelta();
            }
            if (!resurrectTimer.Finished)
            {
                resurrectTimer.TickIntervalDelta();
            }
        }

        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            if (!pawn.IsHashIntervalTick(3600, delta))
            {
                return;
            }
            if (pawn?.mindState?.mentalBreaker?.BreakThresholdMajor != null)
            {
                mentalBreakThreshold = pawn.mindState.mentalBreaker.BreakThresholdMajor;
            }
            if (!pawn.Spawned)
            {
                return;
            }
            lastKnownMood = (lastKnownMood + pawn.needs.mood.CurLevelPercentage) / 2;
            //string newString = pawn.Name + " new tracked mood: " + lastKnownMood + " : " + NormalizedMoodValue + " | " + pawn.mindState.mentalBreaker.BreakThresholdMajor;
            //Messages.Message(newString, MessageTypeDefOf.NeutralEvent);
        }

        public bool SucceedLastStandRoll()
        {
            if (NormalizedMoodValue > 0)
            {
                return Rand.Chance(NormalizedMoodValue); // Normalize 0.5-1.0f -> 0.0-1.0f | Mood below 0.5 is treated as 0.
            }
            return false;
        }
    }
}
