using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.ScenParts
{
    // Referenced from Source_XylRaces by xylthixlm
    public class ScenPart_ConfigPage_ConfigureStartingPawns_KindDefs_Ext : ScenPart_ConfigPage_ConfigureStartingPawns_KindDefs
    {
        protected override void GenerateStartingPawns()
        {
            PawnKindDef referencePawnKindDef = kindCounts.First().kindDef;
            List<XenotypeChance> xenotypeCounts = new List<XenotypeChance>();
            for (int i = 0; i < referencePawnKindDef.xenotypeSet.Count; i++)
            {
                xenotypeCounts.Add(referencePawnKindDef.xenotypeSet[i]);
            }

            foreach (var xenotypeCount in xenotypeCounts)
                xenotypeCount.xenotype ??= XenotypeDefOf.Baseliner;

            base.GenerateStartingPawns();

            while (Find.GameInitData.startingAndOptionalPawns.Count < pawnChoiceCount)
            {
                // Make sure that the extra reserve pawns are appropriate xenotypes
                int index = Find.GameInitData.startingAndOptionalPawns.Count;
                var request = StartingPawnUtility.GetGenerationRequest(index);
                request.ForcedXenotype = xenotypeCounts.RandomElementByWeight(x => x.chance).xenotype;
                StartingPawnUtility.SetGenerationRequest(index, request);

                StartingPawnUtility.AddNewPawn(index);
            }
        }
    }
}
