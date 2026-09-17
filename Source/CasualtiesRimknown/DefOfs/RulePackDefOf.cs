using RimWorld;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class RulePackDefOf
    {
        public static RulePackDef CR_Event_LastStandTriggered;
        static RulePackDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDefOf));
        }
    }
}
