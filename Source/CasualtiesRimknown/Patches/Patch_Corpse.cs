using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace CasualtiesRimknown.Patches
{
    [HarmonyPatch(typeof(Corpse), nameof(Corpse.Destroy))]
    static class Corpse_Destroy_Patch
    {
        internal static void Prefix(Corpse __instance)
        {
            __instance.Map.GetComponent<LastStandManager>().Deregister(__instance);
        }
    }

    [HarmonyPatch(typeof(Corpse), nameof(Corpse.SpawnSetup))]
    static class Corpse_SpawnSetup_Patch
    {
        internal static void Postfix(Corpse __instance, Map map)
        {
            if (__instance?.InnerPawn.genes?.HasActiveGene(DefOfs.GeneDefOf.CR_LastStand) == true)
            {
                Gene_LastStand lastStandGene = (Gene_LastStand)__instance.InnerPawn.genes.GetGene(DefOfs.GeneDefOf.CR_LastStand);
                if (lastStandGene.CouldResurrect)
                {
                    map.GetComponent<LastStandManager>().Register(__instance);
                }
            }
        }
    }
}
