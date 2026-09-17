using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.ThoughtWorkers
{
    // Based off of Rimworld.ThoughtWorker_Ugly
    public class ThoughtWorker_Mindwiped : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
        {
            if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
            {
                return false;
            }
            if (PawnUtility.IsBiologicallyOrArtificiallyBlind(pawn))
            {
                return false;
            }
            if (!other.health.hediffSet.HasHediff(DefOfs.HediffDefOf.CR_Hollow))
            {
                return false;
            }
            return true;
        }
    }
}
