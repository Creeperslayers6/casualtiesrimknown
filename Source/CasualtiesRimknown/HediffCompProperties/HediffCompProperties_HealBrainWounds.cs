using CasualtiesRimknown.HediffComps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.HediffCompProperties
{
    public class HediffCompProperties_HealBrainWounds : Verse.HediffCompProperties
    {
        public float secondsBetweenHealCycle = 1f;
        public float severityReduction = 0.5f;
        public HediffCompProperties_HealBrainWounds()
        {
            compClass = typeof(HediffComp_HealBrainWounds);
        }
    }
}
