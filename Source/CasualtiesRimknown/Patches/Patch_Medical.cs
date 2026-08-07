using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CasualtiesRimknown.Patches
{
    [HarmonyPatch(typeof(TendUtility), nameof(TendUtility.CalculateBaseTendQuality), new Type[] { typeof(Pawn), typeof(Pawn), typeof(float), typeof(float) })]
    static class TendUtility_CalculateBaseTendQuality_Patch
    {
        internal static void Postfix(ref float __result, Pawn doctor, Pawn patient, float medicineQualityMax)
        {
            if (doctor == patient && doctor != null && doctor.health.hediffSet.HasHediff(DefOfs.HediffDefOf.CR_Chipped))
            {
                Log.Message("Self-tend had Chipped Hediff! | Apply 90% Self-Tend Value");
                float num = __result * (9/7); // Chipped Self-Tend Buff 70% -> 90%
                __result = Mathf.Clamp(num, 0f, medicineQualityMax);
            }
        }
    }
}
