using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.Hediffs
{
    public class Hediff_InternalBleeding : Hediff_Injury
    {
        public override string LabelBase => !pawn.RaceProps.IsMechanoid ? base.LabelBase : "TestLabel".Translate();
        public override void PostAdd(DamageInfo? dinfo)
        {
            base.PostAdd(dinfo);
            base.destroysBodyParts = false; // TODO: ADD DESTROY LIMBS SETTING?
         }
    }
}
