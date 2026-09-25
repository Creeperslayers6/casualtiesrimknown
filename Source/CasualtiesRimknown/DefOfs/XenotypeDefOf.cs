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
        // Species: Experiment (SAW-01)
        [MayRequire("Erin.Expie")]
        public static XenotypeDef ERN_Expie;

        [MayRequire("Expieworld.Package")]
        public static XenotypeDef Expie_Xenotype;

        // Species: Milky (SAW-03)
        [MayRequire("Milkyworld.Package")]
        public static XenotypeDef Milky_Xenotype;

        [MayRequire("kotovaann.ernloreexpansion")]
        public static XenotypeDef ERNLE_Milky;

        // Species: Dune (SAW-12)
        [MayRequire("kotovaann.ernloreexpansion")]
        public static XenotypeDef ERNLE_Dune;

        static XenotypeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(XenotypeDefOf));
        }
    }
}
