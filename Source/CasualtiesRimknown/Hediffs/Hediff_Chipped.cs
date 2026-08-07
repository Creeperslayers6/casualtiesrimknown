using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Sound;

namespace CasualtiesRimknown.Hediffs
{
    public class Hediff_Chipped : HediffWithComps
    {
        bool explosiveArmed = false;
        float explosiveTimeToDetonation = 4.2f;
        TickTimer explosiveCountdown = new TickTimer();

        float explosionRadius = 2f;
        int explosionDamage = 80;
        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            if (Severity == 5 && !explosiveArmed)
            {
                Arm_Device();
            }
            if (explosiveArmed && !explosiveCountdown.Finished)
            {
                explosiveCountdown.TickIntervalDelta();
            }
        }
        public void Arm_Device()
        {
            explosiveArmed = true;
            explosiveCountdown.Start(GenTicks.TicksGame, explosiveTimeToDetonation.SecondsToTicks(), Detonate);
            DefOfs.SoundDefOf.CR_BrainChip_SelfDestruct.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
            pawn?.needs?.mood?.thoughts?.memories?.TryGainMemory(DefOfs.ThoughtDefOf.CR_BrainChip_SelfDestructing);
        }

        public override void Notify_Downed()
        {
            Severity = 5;
        }

        private void Detonate()
        {
            Map currentMap = pawn.Map;
            BodyPartRecord pawnBrain = pawn.health.hediffSet.GetBrain();
            pawn.TakeDamage(new DamageInfo(DamageDefOf.Bomb, 50, 0, -1, pawn));
            GenExplosion.DoExplosion(pawn.Position, currentMap, explosionRadius, DamageDefOf.Bomb, pawn, explosionDamage);
            if (pawnBrain != null)
            {
                pawn.TakeDamage(new DamageInfo(DamageDefOf.Bomb, 999, 0, -1, pawn, pawnBrain));
            }
            //Hediff brainInjury = pawn.health.AddHediff(DefOfs.HediffDefOf.Shredded, Part);
            //brainInjury.Severity = 30;
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
}
