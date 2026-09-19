using RimWorld;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class TaleDefOf
    {
        public static TaleDef CR_LastStandTriggered;

        static TaleDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TaleDefOf));
        }
    }
}
