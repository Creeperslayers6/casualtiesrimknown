using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown.AI
{
    public static class Toils_Selfharm
    {
        public static Toil SlashEye()
        {
            Toil toil = ToilMaker.MakeToil("SlashEye");
            toil.initAction = delegate
            {
                BodyPartRecord validEyePart = toil.actor.health.hediffSet.GetNotMissingParts().First((BodyPartRecord p) => p.def == BodyPartDefOf.Eye);
                if (validEyePart != null)
                {
                    Hediff_MissingPart hediff_MissingEye = (Hediff_MissingPart)HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, toil.actor);
                    hediff_MissingEye.lastInjury = DefOfs.HediffDefOf.Scratch;
                    hediff_MissingEye.Part = validEyePart;
                    hediff_MissingEye.IsFresh = true;
                    toil.actor.health.AddHediff(hediff_MissingEye, validEyePart);
                }
            };
            toil.defaultCompleteMode = ToilCompleteMode.Instant;
            return toil;
        }
        public static Toil SlashRandomBodyPart()
        {
            Toil toil = ToilMaker.MakeToil("SlashRandomBodyPart");
            toil.initAction = delegate
            {
                BodyPartRecord randomBodyPart = ValidBodyPartForSlash(toil.actor);
                CutPawnBodyPart(toil.actor, randomBodyPart);
            };
            toil.defaultCompleteMode = ToilCompleteMode.Delay;
            toil.defaultDuration = 150;
            toil.PlaySustainerOrSound(() => SoundDefOf.Bloodfeed_Cast);
            return toil;
        }

        public static Toil SlashRandomBodyPartTerminal()
        {
            Toil toil = ToilMaker.MakeToil("SlashRandomBodyPartTerminal");
            toil.initAction = delegate
            {
                TerminalCutPawnBodyPart(toil.actor);
            };
            toil.defaultCompleteMode = ToilCompleteMode.Instant;
            return toil;
        }

        private static void CutPawnBodyPart(Pawn pawn, BodyPartRecord targetBodyPartRecord)
        {
            float randomPartHealth = targetBodyPartRecord.def.GetMaxHealth(pawn);
            Hediff_Injury slashHediff = (Hediff_Injury)HediffMaker.MakeHediff(DefOfs.HediffDefOf.Scratch, pawn);
            slashHediff.Part = targetBodyPartRecord;
            slashHediff.Severity = randomPartHealth * 0.15f;
            pawn.health.AddHediff(slashHediff, targetBodyPartRecord);
        }

        private static void TerminalCutPawnBodyPart(Pawn pawn)
        {
            BodyPartRecord randomBodyPart = ValidBodyPartForSlash(pawn);
            for (int i = 0; i < 5; i++)
            {
                if (pawn.health.hediffSet.HasMissingPartFor(randomBodyPart))
                {
                    continue;
                }
                CutPawnBodyPart(pawn, randomBodyPart);
            }
        }

        private static BodyPartRecord ValidBodyPartForSlash(Pawn pawn)
        {
            return (pawn.health.hediffSet.GetNotMissingParts(depth: BodyPartDepth.Outside).Where((BodyPartRecord p) => p.def != BodyPartDefOf.Head && p.def.hitPoints >= 30)).RandomElement();
        }
    }
}