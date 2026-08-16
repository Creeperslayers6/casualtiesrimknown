using RimWorld;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class JobDefOf
    {
        public static JobDef CR_VomitBlood;
        static JobDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
        }
    }
}
