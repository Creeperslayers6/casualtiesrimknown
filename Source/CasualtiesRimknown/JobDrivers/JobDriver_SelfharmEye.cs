using CasualtiesRimknown.AI;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown.JobDrivers
{
    public class JobDriver_SelfharmEye : JobDriver_Selfharm
    {
        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_General.WaitWith(TargetIndex.A, WAIT_DURATION.SecondsToTicks(), true, false, false);
            //
            yield return Toils_Selfharm.SlashEye();
        }
    }
}
