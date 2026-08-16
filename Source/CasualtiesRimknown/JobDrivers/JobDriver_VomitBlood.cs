using CasualtiesRimknown.Utilities;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown.JobDrivers
{
    public class JobDriver_VomitBlood : JobDriver_Vomit
    {
        ThingDef filthBlood;
        EffecterDef effectVomitBlood = DefOfs.EffecterDefOf.CR_VomitBloodEffect;
        //
        private int ticksLeft;
        protected override IEnumerable<Toil> MakeNewToils()
        {
            filthBlood = VomitBloodUtility.GetFilthBlood(pawn);
            Color filthBloodColor = filthBlood.graphicData.color;
            //
            Toil toil = ToilMaker.MakeToil("MakeNewToils");
            toil.initAction = delegate
            {
                ticksLeft = Rand.Range(300, 900);
                int num = 0;
                IntVec3 intVec;
                do
                {
                    intVec = pawn.Position + GenAdj.AdjacentCellsAndInside[Rand.Range(0, 9)];
                    num++;
                    if (num > 12)
                    {
                        intVec = pawn.Position;
                        break;
                    }
                }
                while (!intVec.InBounds(pawn.Map) || !intVec.Standable(pawn.Map));
                job.targetA = intVec;
                pawn.pather.StopDead();
            };
            toil.tickIntervalAction = delegate (int delta)
            {
                if (pawn.IsHashIntervalTick(150, delta))
                {
                    FilthMaker.TryMakeFilth(job.targetA.Cell, base.Map, ThingDefOf.Filth_Vomit, pawn.LabelIndefinite());
                    if (pawn.needs != null && pawn.needs.TryGetNeed(out Need_Food need) && need.CurLevelPercentage > 0.1f)
                    {
                        need.CurLevel -= need.MaxLevel * 0.04f;
                    }
                }
                ticksLeft -= delta;
                if (ticksLeft <= 0)
                {
                    ReadyForNextToil();
                    TaleRecorder.RecordTale(TaleDefOf.Vomited, pawn);
                }
            };
            toil.defaultCompleteMode = ToilCompleteMode.Never;
            toil.WithEffect(effectVomitBlood, TargetIndex.A);
            toil.PlaySustainerOrSound(() => SoundDefOf.Vomit);
            yield return toil;
        }
    }
}
