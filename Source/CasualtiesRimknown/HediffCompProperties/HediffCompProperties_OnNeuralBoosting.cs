using CasualtiesRimknown.HediffComps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.HediffCompProperties
{
    public class HediffCompProperties_OnNeuralBoosting : Verse.HediffCompProperties
    {
        public float neuralBoostPenaltySeverity = 1.0f;
        public HediffCompProperties_OnNeuralBoosting()
        {
            compClass = typeof(HediffComp_OnNeuralBoosting);
        }
    }
}
