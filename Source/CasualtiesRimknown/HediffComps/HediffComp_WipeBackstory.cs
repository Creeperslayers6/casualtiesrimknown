using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.HediffComps
{
    public class HediffComp_WipeBackstory : HediffComp
    {
        public HediffCompProperties_WipeBackstory Props => (HediffCompProperties_WipeBackstory)props;
        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            Trigger();
        }

        public void Trigger()
        {
            Pawn.story.Childhood = DefOfs.BackstoryDefOf.CR_ForgottenChildhood;
            Pawn.story.Adulthood = DefOfs.BackstoryDefOf.CR_ForgottenAdulthood;
            Pawn.skills.Notify_SkillDisablesChanged();
        }
    }
}
