using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using HarmonyLib;
using Multiplayer.API;

namespace CasualtiesRimknown
{
    public class CasualtiesRimknown_Mod : Mod
    {
        private static bool isMultiplayerActive;

        public static bool IsMultiplayerActive => isMultiplayerActive;
        public CasualtiesRimknown_Mod(ModContentPack content) : base(content)
        {
            isMultiplayerActive = ModLister.AnyModActiveNoSuffix(["rwmt.Multiplayer"]);
            new Harmony(base.Content.PackageIdPlayerFacing).PatchAll();
        }
    }

    [StaticConstructorOnStartup]
    public static class Casualties_MultiplayerCompat
    {
        static Casualties_MultiplayerCompat()
        {
            if (CasualtiesRimknown_Mod.IsMultiplayerActive)
            {
                RegisterMultiplayer();
            }
        }

        private static void RegisterMultiplayer()
        {
            if (!MP.enabled) return;

            MP.RegisterAll();
        }
    }
}
