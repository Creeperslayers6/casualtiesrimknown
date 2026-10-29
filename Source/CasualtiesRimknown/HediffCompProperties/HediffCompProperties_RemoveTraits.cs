using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using CasualtiesRimknown.HediffComps;

namespace CasualtiesRimknown.HediffCompProperties
{
    public class HediffCompProperties_RemoveTraits : Verse.HediffCompProperties
    {
        public float severity = 1f;

        public bool manuallyTriggered;

        public List<TraitDef> traits = new List<TraitDef>();

        public HediffCompProperties_RemoveTraits()
        {
            compClass = typeof(HediffComp_RemoveTraits);
        }
    }
}
