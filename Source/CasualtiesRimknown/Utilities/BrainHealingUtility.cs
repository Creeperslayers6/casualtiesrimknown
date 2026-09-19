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
                if (pawn.health.hediffSet.hediffs.Where((Hediff hediff) => hediff.Part == pawnBrain && hediff.IsPermanent()).TryRandomElement(out var result))
                {
                    result.Severity -= severityReduction;
                }
            }
        }
    }
}
