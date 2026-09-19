using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.HediffComps
{
    public class HediffComp_MindwipeOnSeverity : HediffComp
    {
        public HediffCompProperties_MindwipeOnSeverity Props => (HediffCompProperties_MindwipeOnSeverity)props;

        private bool triggered = false;

        // Check if Severity increased above 1
        public override void CompPostMerged(Hediff other)
        {
            if (parent.Severity >= Props.mindwipeSeverity && triggered == false)
            {
                triggered = true;
                MindwipePawn();
            }
        }

        private void MindwipePawn()
        {
            Hediff mindwipe = HediffMaker.MakeHediff(DefOfs.HediffDefOf.CR_Hollow, Pawn);
            mindwipe.Severity = 0.01f;
            Pawn.health.AddHediff(mindwipe);

            parent.Severity = 1f;
        }
    }
}
