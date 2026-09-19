using CasualtiesRimknown.MentalStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;

using Verse;
using Verse.AI;
using CasualtiesRimknown.DefOfs;

namespace CasualtiesRimknown.JobGivers
{
    public class JobGiver_SeekingMindwipe : ThinkNode_JobGiver
    {
        // Based on JobGiver_MurderousRage & JobGiver_Binge && JobGiver_BingeDrug
        protected bool IgnoreForbid(Pawn pawn)
        {
            return pawn.InMentalState;
        }

        protected override Job TryGiveJob(Pawn pawn)
        {
            if (!(pawn.MentalState is MentalState_SeekingMindwipe mentalState_SeekingMindwipe))
            {
                return null;
            }
            Thing mindwipe = BestIngestTarget(pawn);
            if (mindwipe == null)
            {
                return null;
            }
            Job job = JobMaker.MakeJob(RimWorld.JobDefOf.Ingest, mindwipe);
            job.count = 1;
            job.ignoreForbidden = IgnoreForbid(pawn);
            job.canBashDoors = true;
            job.overeat = true;
            return job;
        }

        Thing BestIngestTarget(Pawn pawn)
        {
            Predicate<Thing> validator = delegate (Thing t)
            {
                if (!IgnoreForbid(pawn) && t.IsForbidden(pawn))
                {
                    return false;
                }
                if (!pawn.CanReserve(t))
                {
                    return false;
                }
                if (!pawn.Position.InHorDistOf(t.Position, 60f) && !t.Position.Roofed(t.Map) && !pawn.Map.areaManager.Home[t.Position] && t.GetSlotGroup() == null)
                {
                    return false;
                }
                return true;
            };
            return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(DefOfs.ThingDefOf.CR_Mindwipe), PathEndMode.OnCell, TraverseParms.For(pawn, canBashDoors: true, canBashFences: true), 9999f, validator);
        }
    }
}
