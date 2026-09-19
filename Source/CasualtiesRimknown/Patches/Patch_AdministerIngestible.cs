using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.Patches
{
    [HarmonyPatch(typeof(Recipe_AdministerIngestible), nameof(Recipe_AdministerIngestible.IsViolationOnPawn))]
    static class Patch_AdministerIngestible
    {
        internal static bool Prefix(Recipe_AdministerIngestible __instance, ref bool __result, Pawn pawn, Faction billDoerFaction)
        {
            if (pawn.Faction == billDoerFaction)
            {
                return true;
            }
            ThingDef thingDef = __instance.recipe.ingredients[0].filter.AllowedThingDefs.First();
            if (thingDef == DefOfs.ThingDefOf.CR_Mindwipe)
            {
                __result = true;
                return false;
            }
            
            //TODO: ADD CHECK FOR RAPID NEURON REGEN SICKNESS OUTCOMEDOER_GIVEHEDIFF
                // APPLYING RNRS DRUG IS A VIOLATION!

            // No Special Conditions for C:R, Continue As Normal!
            return true;
        }
    }
}
