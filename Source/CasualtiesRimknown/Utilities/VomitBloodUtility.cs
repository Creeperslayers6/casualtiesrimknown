using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.Utilities
{
    [StaticConstructorOnStartup]
    public static class VomitBloodUtility
    {
        private static List<GeneDef> customBloodGenes = new List<GeneDef>();

        static VomitBloodUtility()
        {
            foreach (var def in DefDatabase<GeneDef>.AllDefsListForReading.Where(testGene => testGene.HasModExtension<VEF.Genes.GeneExtension>() && testGene.GetModExtension<VEF.Genes.GeneExtension>().customBloodThingDef != null))
            {
                customBloodGenes.Add(def);
            }
        }

        public static bool PawnHasActiveGenes(Pawn pawn, out GeneDef activeGene)
        {
            foreach (GeneDef bloodGene in customBloodGenes)
            {
                if (pawn.genes.HasActiveGene(bloodGene))
                {
                    activeGene = bloodGene;
                    return true;
                }
            }
            activeGene = null;
            return false;
        }

        public static ThingDef GetFilthBlood(Pawn targetPawn)
        {
            if (customBloodGenes.Count >= 1 && PawnHasActiveGenes(targetPawn, out GeneDef activeGene))
            {
                return activeGene.GetModExtension<VEF.Genes.GeneExtension>().customBloodThingDef;
            }
            return ThingDefOf.Filth_Blood;
        }
    }
}
