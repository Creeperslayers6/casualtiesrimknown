using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.Utilities
{
    public static class SawianUtilty
    {
        // Return true is Pawn is a valid Sawian Xenotype/Species.
        public static bool IsASawianXenotype(this Pawn pawn)
        {
            if (pawn.genes?.Xenotype == null)
            {
                return false;
            }
            // Erin's Experiments
            if (pawn.genes?.Xenotype == DefOfs.XenotypeDefOf.ERN_Expie)
            {
                return true;
            }
            // Gexie's Experiments & Milkies
            if (pawn.genes?.Xenotype == DefOfs.XenotypeDefOf.Expie_Xenotype)
            {
                return true;
            }
            if (pawn.genes?.Xenotype == DefOfs.XenotypeDefOf.Milky_Xenotype)
            {
                return true;
            }
            // Kotovaann's Milky & Dune
            if (pawn.genes?.Xenotype == DefOfs.XenotypeDefOf.ERNLE_Milky)
            {
                return true;
            }
            if (pawn.genes?.Xenotype == DefOfs.XenotypeDefOf.ERNLE_Dune)
            {
                return true;
            }
            return false;
        }
    }
}
