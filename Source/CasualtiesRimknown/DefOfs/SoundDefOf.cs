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
    public static class SoundDefOf
    {
        // Chipped Hediff
        public static SoundDef CR_BrainChip_SelfDestruct;

        // SoundCannon ThingDef
        public static SoundDef CR_SoundCannon_Charging;
        public static SoundDef CR_SoundCannon_Blast;
        public static SoundDef CR_SoundCannon_BlastLoud;

        static SoundDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(SoundDefOf));
        }
    }
}
