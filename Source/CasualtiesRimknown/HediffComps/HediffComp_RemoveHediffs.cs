using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown
{
    public class HediffComp_RemoveHediffs : HediffComp
    {
        public HediffCompProperties_RemoveHediffs Props => (HediffCompProperties_RemoveHediffs)props;

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            Trigger();
        }

        public void Trigger()
        {
            foreach (HediffDef hediff in Props.hediffs)
            {
                RemoveHediff(Pawn, hediff);
            }
        }

        private void RemoveHediff(Pawn pawn, HediffDef targetHediff)
        {
            Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(targetHediff);
            if (firstHediffOfDef != null)
            {
                pawn.health.RemoveHediff(firstHediffOfDef);
            }
        }
    }
}
