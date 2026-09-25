using HarmonyLib;
using Verse;

namespace CasualtiesRimknown.Patches
{
    [HarmonyPatch(typeof(UIRoot_Entry), nameof(UIRoot_Entry.Init))]
    static class UIRootEntry_Init_Patch
    {
        private static bool _warnedAboutMissingXenotypeMod;
        internal static void Prefix()
        {
            if (!_warnedAboutMissingXenotypeMod)
            {
                bool isSawianXenotypeModLoaded = ModLister.AnyModActiveNoSuffix([
                    "Erin.Expie",
                    "Expieworld.Package",
                    "Milkyworld.Package",
                    ]);
                if (!isSawianXenotypeModLoaded)
                {
                    Dialog_MessageBox dialog = new Dialog_MessageBox("CR_Menu_MissingXenotypeModDialog".Translate());
                    Find.WindowStack.Add(dialog);
                    _warnedAboutMissingXenotypeMod = true;
                }
            }
        }
    }
}
