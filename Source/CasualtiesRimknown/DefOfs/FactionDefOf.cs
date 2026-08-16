using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class FactionDefOf
    {
        public static FactionDef CR_TheCompany;
        static FactionDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FactionDefOf));
        }
    }
}
