using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class ThingDefOf
    {
        public static ThingDef CR_Mindwipe;

        public static ThingDef CR_Turret_SoundCannonBasic;
        public static ThingDef CR_Skyfaller_SoundCannonIncoming;
        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }
    }
}
