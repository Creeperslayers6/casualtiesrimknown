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

        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Expie_Xenotype;

        // Species: Orange (SAW-02)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Orange_Xenotype;

        // Species: Milky (SAW-03)
        [MayRequire("Milkyworld.Package")]
        public static XenotypeDef Milky_Xenotype;

        [MayRequire("kotovaann.ernloreexpansion")]
        public static XenotypeDef ERNLE_Milky;

        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Milky_Xenotype;

        // Species: Leapy (SAW-04)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Leapy_Xenotype;

        // Species: Chompy (SAW-05)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Chompy_Xenotype;

        // Species: Roza (SAW-06)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Roza_Xenotype;

        // Species: Voyager (SAW-07)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Voyager_Xenotype;

        // Species: Baron (SAW-08)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Baron_Xenotype;

        // Species: Chik (SAW-09)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Chik_Xenotype;

        // Species: Crystal (SAW-10)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Crystal_Xenotype;

        // Species: Velvet (SAW-11)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Velvet_Xenotype;

        // Species: Dune (SAW-12)
        [MayRequire("kotovaann.ernloreexpansion")]
        public static XenotypeDef ERNLE_Dune;

        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Dune_Xenotype;

        // Species: Shelly (SAW-13)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Shelly_Xenotype;

        // Species: Icke (SAW-14)
        [MayRequire("yourimaine.GunsawXenotypes")]
        public static XenotypeDef GX_Icke_Xenotype;

        // Species: G4-A
        [MayRequire("yourimaine.GunsawXenotypes, vanillaracesexpanded.android"), DefAlias("GX_G4-A-Awakened_Xenotype")]
        public static XenotypeDef GX_G4A_Awakened_Xenotype;

        static XenotypeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(XenotypeDefOf));
        }
    }
}
