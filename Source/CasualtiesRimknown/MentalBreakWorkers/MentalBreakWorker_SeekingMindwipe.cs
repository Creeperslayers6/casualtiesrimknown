using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown.MentalBreakWorkers
{
    public class MentalBreakWorker_SeekingMindwipe : MentalBreakWorker
    {
        public override bool BreakCanOccur(Pawn pawn)
        {
            if (!pawn.Spawned)
            {
                return false;
            }
            if (!base.BreakCanOccur(pawn))
            {
                return false;
            }
            //
            List<Thing> list = pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.Drug);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].def.defName == "CR_Mindwipe" && !list[i].Position.Fogged(list[i].Map) && (list[i].Position.Roofed(list[i].Map) || list[i].Position.InHorDistOf(pawn.Position, 45f)) && pawn.CanReach(list[i], PathEndMode.ClosestTouch, Danger.Deadly, canBashDoors: true, canBashFences: true))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
