using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class HediffDefOf
    {
        // Rimworld HediffDef
        public static HediffDef Scratch;

        // Casualties: Rimknown HediffDef
        public static HediffDef CR_MuscleStrain;

        public static HediffDef CR_Hollow;
        public static HediffDef CR_Chipped;

        public static HediffDef CR_InternalBleeding;
        public static HediffDef CR_Hemothorax;
        public static HediffDef CR_DeepCut;
        static HediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
        }
    }
}
