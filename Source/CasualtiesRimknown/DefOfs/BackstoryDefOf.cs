using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class BackstoryDefOf
    {
        public static BackstoryDef CR_ForgottenChildhood;
        public static BackstoryDef CR_ForgottenAdulthood;
        static BackstoryDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BackstoryDefOf));
        }
    }
}
