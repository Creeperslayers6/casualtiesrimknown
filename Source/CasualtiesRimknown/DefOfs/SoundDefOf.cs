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
        public static SoundDef CR_BrainChip_SelfDestruct;
        static SoundDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(SoundDefOf));
        }
    }
}
