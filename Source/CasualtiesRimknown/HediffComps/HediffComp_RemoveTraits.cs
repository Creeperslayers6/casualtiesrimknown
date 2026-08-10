using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown
{
    public class HediffComp_RemoveTraits : HediffComp
    {
        public HediffCompProperties_RemoveTraits Props => (HediffCompProperties_RemoveTraits)props;

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            if (!Props.manuallyTriggered && !(parent.Severity < Props.severity))
            {
                Trigger();
            }
        }

        public void Trigger()
        {
            foreach (TraitDef traitdef in Props.traits)
            {
                RemoveTrait(Pawn, traitdef);
            }
        }

        private void RemoveTrait(Pawn pawn, TraitDef targetTraitDef)
        {
            if (pawn.story != null)
            {
                Trait trait = pawn.story.traits.GetTrait(targetTraitDef);
                if (trait != null)
                {
                    pawn.story.traits.RemoveTrait(trait, true);
                }
            }
        }
    }
}
