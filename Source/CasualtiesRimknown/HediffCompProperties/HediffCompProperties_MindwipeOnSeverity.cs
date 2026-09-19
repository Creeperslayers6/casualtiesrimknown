using CasualtiesRimknown.HediffComps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown
{
    public class HediffCompProperties_MindwipeOnSeverity : HediffCompProperties
    {
        public float mindwipeSeverity = 1.5f;

        public HediffCompProperties_MindwipeOnSeverity()
        {
            compClass = typeof(HediffComp_MindwipeOnSeverity);
        }
    }
}
