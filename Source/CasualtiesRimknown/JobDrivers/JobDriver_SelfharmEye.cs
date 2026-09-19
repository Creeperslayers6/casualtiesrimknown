using CasualtiesRimknown.AI;
using CasualtiesRimknown.MentalStates;
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
            Toil waitToil = Toils_General.Wait(WAIT_DURATION.SecondsToTicks());
            waitToil.WithProgressBarToilDelay(TargetIndex.A, WAIT_DURATION.SecondsToTicks());
            yield return waitToil;
            //
            yield return Toils_Selfharm.SlashEye();

            Toil toil = Toils_General.Do(delegate
            {
                if(pawn.MentalState is MentalState_SelfharmEvent selfHarmMentalState)
                {
                    selfHarmMentalState?.Notify_FinishedAction();
                }
            });
            yield return toil;
        }
    }
}
