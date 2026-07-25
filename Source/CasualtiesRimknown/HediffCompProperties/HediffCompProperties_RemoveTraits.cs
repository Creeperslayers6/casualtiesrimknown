using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown
{
    public class HediffCompProperties_RemoveTraits : HediffCompProperties
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
