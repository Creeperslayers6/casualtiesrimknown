using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace CasualtiesRimknown
{
    public class HediffComp_SetSkills : HediffComp
    {
        public HediffCompProperties_SetSkills Props => (HediffCompProperties_SetSkills)props;

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            Trigger();
        }

        public void Trigger()
        {
            foreach (HediffCompProperties_SetSkills.SkillValue skillValue in Props.skills)
            {
                SetSkill(skillValue.skill, skillValue.amount);
            }
        }

        private void SetSkill(SkillDef skillDef, int skillLevel)
        {
            SkillRecord skill = Pawn.skills.GetSkill(skillDef);
            skill.Level = skillLevel;
            skill.xpSinceMidnight = 0f;
            skill.xpSinceLastLevel = 0f;
            skill.passion = Passion.None;
        }
    }
}
