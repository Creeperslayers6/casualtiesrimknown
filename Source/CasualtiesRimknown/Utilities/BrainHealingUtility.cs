using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.Utilities
{
    public static class BrainHealingUtility
    {
        public static void TryHealRandomBrainWound(Pawn pawn, float severityReduction)
        {
            BodyPartRecord pawnBrain = pawn.health.hediffSet.GetBrain();
            if (pawnBrain != null)
            {
                Hediff randomBrainInjury = pawn.health.hediffSet.hediffs.Where((Hediff hediff) => hediff.Part == pawnBrain && hediff.IsPermanent()).RandomElement();
                if (randomBrainInjury != null)
                {
                    Log.Message("Run1");
                    randomBrainInjury.Severity -= severityReduction;
                }
                if (pawn.health.hediffSet.TryGetHediff(DefOfs.HediffDefOf.TraumaSavant, out var traumaSavant))
                {
                    Log.Message("Run2");
                    HealthUtility.Cure(traumaSavant);
                }
                //if (pawn.health.hediffSet.hediffs.Where((Hediff hediff) => hediff.Part == pawnBrain && hediff.IsPermanent()).TryRandomElement(out var result))
                //{
                //    result.Severity -= severityReduction;
                //}
                //else if(pawn.health.hediffSet.TryGetHediff(DefOfs.HediffDefOf.TraumaSavant, out var traumaSavant))
                //{
                //    HealthUtility.Cure(traumaSavant);
                //}
            }
        }
    }
}
