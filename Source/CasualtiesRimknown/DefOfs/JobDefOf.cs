using RimWorld;
using Verse;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class JobDefOf
    {
        public static JobDef CR_VomitBlood;
        //
        public static JobDef CR_SelfharmSlashEye;
        public static JobDef CR_SelfharmSlashOnce;
        public static JobDef CR_SelfharmSlashTerminal;
        static JobDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
        }
    }
}
