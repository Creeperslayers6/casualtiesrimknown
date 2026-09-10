using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasualtiesRimknown.DefOfs
{
    [DefOf]
    public static class HistoryEventDefOf
    {
        public static HistoryEventDef CR_AcceptedSawianXenotype;
        static HistoryEventDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HistoryEventDefOf));
        }
    }
}
