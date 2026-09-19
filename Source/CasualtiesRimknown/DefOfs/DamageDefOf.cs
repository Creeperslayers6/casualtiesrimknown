using RimWorld;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class DamageDefOf
    {
        public static DamageDef CR_SonicShockwave;
        static DamageDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DamageDefOf));
        }
    }
}
