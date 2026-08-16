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
        public static Mod_Settings settings;
        private static bool isMultiplayerActive;

        public static bool IsMultiplayerActive => isMultiplayerActive;
        public CasualtiesRimknown_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<Mod_Settings>();
            //
            isMultiplayerActive = ModLister.AnyModActiveNoSuffix(["rwmt.Multiplayer"]);
            //
            new Harmony(base.Content.PackageIdPlayerFacing).PatchAll();
        }

        public override string SettingsCategory()
        {
            return "CasualtiesRimknownName".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard ListA = new Listing_Standard();
            ListA.Begin(inRect);
            //
            ListA.Label("CR_SettingLabel_Company".Translate());
            ListA.CheckboxLabeled("CR_Setting_CompanySendsTraps".Translate(), ref settings.storytellerCompanySendsTraps, "CR_Setting_CompanySendsTraps_TT".Translate());
            ListA.CheckboxLabeled("CR_Setting_SilentTrapDeployment".Translate(), ref settings.storytellerSilentTrapDeployment, "CR_Setting_SilentTrapDeployment_TT".Translate());
            //
            ListA.GapLine();
            ListA.Label("CR_SettingLabel_TrapSoundCannon".Translate());
            ListA.CheckboxLabeled("CR_Setting_TrapSoundCannon_ShockwaveDestroysLimbs".Translate(), ref settings.soundCannon_ShockwaveDestroysLimbs, "CR_Setting_TrapSoundCannon_ShockwaveDestroysLimbs_TT".Translate());
            ListA.CheckboxLabeled("CR_Setting_TrapSoundCannon_UseLoudBlastEffect".Translate(), ref settings.soundCannon_UseLoudBlastEffect, "CR_Setting_TrapSoundCannon_UseLoudBlastEffect_TT".Translate());
            //
            ListA.End();
            //
            base.DoSettingsWindowContents(inRect);
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

    public class Mod_Settings : ModSettings
    {
        // The Company™
        public bool storytellerCompanySendsTraps = true;
        public bool storytellerSilentTrapDeployment = true;
        // Sound Cannon
        public bool soundCannon_ShockwaveDestroysLimbs = false;
        public bool soundCannon_UseLoudBlastEffect = false;
        public override void ExposeData()
        {
            // The Company Settings
            Scribe_Values.Look(ref storytellerCompanySendsTraps, "storyteller_company_sends_traps", true);
            Scribe_Values.Look(ref storytellerSilentTrapDeployment, "storyteller_silent_trap_deployment", true);
            // Sound Cannon
            Scribe_Values.Look(ref soundCannon_ShockwaveDestroysLimbs, "soundcannon_shockwave_destroys_limbs", false);
            Scribe_Values.Look(ref soundCannon_UseLoudBlastEffect, "soundcannon_use_loud_blast_effect", false);
            //
            base.ExposeData();
        }
    }
}
