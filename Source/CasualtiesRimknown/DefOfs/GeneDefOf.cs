using RimWorld;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class GeneDefOf
    {
        public static GeneDef CR_LastStand;
        static GeneDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GeneDefOf));
        }
    }
}
