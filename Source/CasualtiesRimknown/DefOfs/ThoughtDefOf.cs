using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class ThoughtDefOf
    {
        public static ThoughtDef CR_BrainChip_SelfDestructing;
        static ThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
        }
    }
}
