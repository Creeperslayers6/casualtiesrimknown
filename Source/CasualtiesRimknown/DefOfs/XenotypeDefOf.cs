using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class XenotypeDefOf
    {
        public static XenotypeDef ERN_Expie;

        static XenotypeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(XenotypeDefOf));
        }
    }
}
