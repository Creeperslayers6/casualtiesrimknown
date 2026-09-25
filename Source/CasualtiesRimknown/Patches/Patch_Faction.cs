using CasualtiesRimknown.Utilities;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.Patches
{
    [HarmonyPatch(typeof(Faction), nameof(Faction.Notify_PawnJoined))]
    static class Faction_Notify_PawnJoined_Patch
    {
        public static Faction PlayerFaction => Faction.OfPlayerSilentFail ?? Find.GameInitData.playerFaction;

        internal static void Postfix(Faction __instance,Pawn p)
        {
            if (__instance == PlayerFaction && p.IsASawianXenotype())
            {
                Faction theCompany = Find.FactionManager.FirstFactionOfDef(DefOfs.FactionDefOf.CR_TheCompany);
                //
                FactionRelationKind playerRelationKind = theCompany.PlayerRelationKind;
                //
                int goodWillChangeAmount = PlayerFaction.GoodwillToMakeHostile(theCompany);
                HistoryEventDef hostilityReason = DefOfs.HistoryEventDefOf.CR_AcceptedSawianXenotype;
                PlayerFaction.TryAffectGoodwillWith(theCompany, goodWillChangeAmount, true, true, hostilityReason);
                //
                TaggedString text = "";
                theCompany.TryAppendRelationKindChangedInfo(ref text, playerRelationKind, theCompany.PlayerRelationKind);
                if (!text.NullOrEmpty())
                {
                    text = "\n\n" + text;
                }
            }
        }
    }
}
