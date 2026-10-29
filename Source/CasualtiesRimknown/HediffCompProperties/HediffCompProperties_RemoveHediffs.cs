using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using CasualtiesRimknown.HediffComps;

namespace CasualtiesRimknown.HediffCompProperties
{
    public class HediffCompProperties_RemoveHediffs : Verse.HediffCompProperties
    {
        public float severity = 1f;

        public bool manuallyTriggered;

        public List<HediffDef> hediffs = new List<HediffDef>();

        public HediffCompProperties_RemoveHediffs()
        {
            compClass = typeof(HediffComp_RemoveHediffs);
        }
    }
}
