using CasualtiesRimknown.Utilities;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.IngestionOutcomeDoers
{
    public class IngestionOutcomeDoer_HealBrainWounds : IngestionOutcomeDoer
    {
        public float severityReduction = 0.5f;
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            for (int i =  0; i < 3; i++)
            {
                BrainHealingUtility.TryHealRandomBrainWound(pawn, severityReduction);
            }
        }
    }
}
