using CasualtiesRimknown.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.HediffComps
{
    // Based on Verse.HediffComp_HealPermanentWounds
    public class HediffComp_HealBrainWounds : HediffComp
    {
        private int ticksToHeal;
        public HediffCompProperties_HealBrainWounds Props => (HediffCompProperties_HealBrainWounds)props;

        private void ResetTicksToHeal()
        {
            ticksToHeal = Props.secondsBetweenHealCycle.SecondsToTicks();
        }

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            ticksToHeal -= delta;
            if (ticksToHeal <= 0)
            {
                BrainHealingUtility.TryHealRandomBrainWound(base.Pawn, Props.severityReduction);
                ResetTicksToHeal();
            }
        }

        public override void CompExposeData()
        {
            Scribe_Values.Look(ref ticksToHeal, "ticksToHeal", 0);
        }

        public override string CompDebugString()
        {
            return "ticksToHeal: " + ticksToHeal;
        }
    }
}
