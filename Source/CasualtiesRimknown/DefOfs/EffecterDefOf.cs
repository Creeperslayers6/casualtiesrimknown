using RimWorld;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class EffecterDefOf
    {
        public static EffecterDef CR_VomitBloodEffect;

        static EffecterDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(EffecterDefOf));
        }
    }
}
