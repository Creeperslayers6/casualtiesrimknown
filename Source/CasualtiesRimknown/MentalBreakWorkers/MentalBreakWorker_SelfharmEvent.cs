using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown.MentalBreakWorkers
{
    public class MentalBreakWorker_SelfharmEvent : MentalBreakWorker
    {
        public override bool BreakCanOccur(Pawn pawn)
        {
            // Add Setting Check For Self-Harm!!!!
            return base.BreakCanOccur(pawn);
        }
        public override float CommonalityFor(Pawn pawn, bool moodCaused = false)
        {
            if (pawn.genes.Xenotype != DefOfs.XenotypeDefOf.ERN_Expie)
            {
                return 0f;
            }
            return base.CommonalityFor(pawn, moodCaused);
        }
    }
}
