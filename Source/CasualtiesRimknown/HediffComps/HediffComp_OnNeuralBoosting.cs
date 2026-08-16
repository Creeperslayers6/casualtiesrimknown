using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.HediffComps
{
    public class HediffComp_OnNeuralBoosting : HediffComp
    {
        public HediffCompProperties_OnNeuralBoosting Props => (HediffCompProperties_OnNeuralBoosting)props;

        public override void CompPostMerged(Hediff other)
        {
            if (parent.Severity > Props.neuralBoostPenaltySeverity)
            {
                // +100 SICKNESS
                // -30 BRAIN HEALTH
                // +0.855L/m Internal Bleeding
                BodyPartRecord pawnTorso = base.Pawn.health.hediffSet.GetBodyPartRecord(BodyPartDefOf.Torso);
                Hediff internalBleeding = HediffMaker.MakeHediff(DefOfs.HediffDefOf.CR_InternalBleeding, base.Pawn, pawnTorso);
                internalBleeding.Severity = 15f;
                base.Pawn.health.AddHediff(internalBleeding);

                // Code from RimWorld.ResurrectionUtility - TryResurrectWithSideEffects(Pawn pawn)
                foreach (BodyPartRecord item in from bodyPartRecord in base.Pawn.health.hediffSet.GetNotMissingParts()
                                                where bodyPartRecord.def == BodyPartDefOf.Eye
                                                select bodyPartRecord)
                {
                    if (!base.Pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(item))
                    {
                        Hediff hediff3 = HediffMaker.MakeHediff(HediffDefOf.Blindness, base.Pawn, item);
                        base.Pawn.health.AddHediff(hediff3);
                    }
                }
                // Ragdoll! 1s
                base.Pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Vomit), Verse.AI.JobCondition.InterruptForced, null, resumeCurJobAfterwards: true);
            }
        }
    }
}
