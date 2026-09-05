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
    public abstract class JobDriver_Selfharm : JobDriver
    {
        public const float WAIT_DURATION = 5f;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }
    }
}
