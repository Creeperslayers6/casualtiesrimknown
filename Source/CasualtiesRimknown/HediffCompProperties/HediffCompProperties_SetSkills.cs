using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RimWorld;
using Verse;

namespace CasualtiesRimknown
{
    public class HediffCompProperties_SetSkills : HediffCompProperties
    {
        public class SkillValue
        {
            public SkillDef skill;

            public int amount;

            public void LoadDataFromXmlCustom(XmlNode xmlRoot)
            {
                DirectXmlCrossRefLoader.RegisterObjectWantsCrossRef(this, "skill", xmlRoot.Name);
                if (xmlRoot.HasChildNodes)
                {
                    amount = ParseHelper.FromString<int>(xmlRoot.FirstChild.Value);
                }
            }
        }

        public float severity = 1f;

        public bool manuallyTriggered;

        public List<SkillValue> skills = new List<SkillValue>();

        public HediffCompProperties_SetSkills()
        {
            compClass = typeof(HediffComp_SetSkills);
        }
    }
}
