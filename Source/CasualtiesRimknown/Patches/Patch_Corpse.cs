using CasualtiesRimknown.Genes;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CasualtiesRimknown.Patches
{
    [HarmonyPatch(typeof(Corpse), nameof(Corpse.TickRare))]
    static class Corpse_TickRare_Patch 
    { 
        internal static void Postfix(Corpse __instance)
        {
            if (__instance.Destroyed || __instance.GetRotStage() != RotStage.Fresh)
            {
                return;
            }
            Gene_LastStand lastStandGene = (Gene_LastStand)__instance?.InnerPawn?.genes?.GetGene(DefOfs.GeneDefOf.CR_LastStand);
            if (lastStandGene != null && __instance.Spawned)
            {
                lastStandGene.TickRare();
            }
        }
    }
}