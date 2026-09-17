using CasualtiesRimknown;
using HarmonyLib;
using Multiplayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

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
            Listing_Standard Main_List = new Listing_Standard();
            Main_List.Begin(inRect);
            float columnWidth = (inRect.width - 34f) / 2f;
            Main_List.ColumnWidth = columnWidth;

            // Section Dedicated to General Settings.
            Listing_Standard sectionGeneral = Main_List.BeginSection(200);
            sectionGeneral.Label("CR_SettingLabel_General".Translate());
            sectionGeneral.GapLine();
            sectionGeneral.CheckboxLabeled("CR_Setting_General_TriggeringContentToggle".Translate(), ref settings.triggerContent_toggle, "CR_Setting_General_TriggeringContentToggle_TT".Translate());
            Main_List.EndSection(sectionGeneral);
            Main_List.Gap();

            // Section Dedicated to The Company™.
            Listing_Standard sectionTheCompany = Main_List.BeginSection(200);
            sectionTheCompany.Label("CR_SettingLabel_Company".Translate());
            sectionTheCompany.GapLine();
            sectionTheCompany.CheckboxLabeled("CR_Setting_CompanySendsTraps".Translate(), ref settings.storytellerCompanySendsTraps, "CR_Setting_CompanySendsTraps_TT".Translate());
            sectionTheCompany.CheckboxLabeled("CR_Setting_SilentTrapDeployment".Translate(), ref settings.storytellerSilentTrapDeployment, "CR_Setting_SilentTrapDeployment_TT".Translate());
            Main_List.EndSection(sectionTheCompany);
            Main_List.Gap();

            // Section Dedicated to Buildings.
            Listing_Standard sectionBuildings = Main_List.BeginSection(200);
            sectionBuildings.Label("CR_SettingLabel_TrapSoundCannon".Translate());
            sectionBuildings.GapLine();
            sectionBuildings.CheckboxLabeled("CR_Setting_TrapSoundCannon_ShockwaveDestroysLimbs".Translate(), ref settings.soundCannon_ShockwaveDestroysLimbs, "CR_Setting_TrapSoundCannon_ShockwaveDestroysLimbs_TT".Translate());
            sectionBuildings.CheckboxLabeled("CR_Setting_TrapSoundCannon_UseLoudBlastEffect".Translate(), ref settings.soundCannon_UseLoudBlastEffect, "CR_Setting_TrapSoundCannon_UseLoudBlastEffect_TT".Translate());
            Main_List.EndSection(sectionBuildings);
            Main_List.Gap();

            // Section Dedicated to Triggering Content.
            if (settings.triggerContent_toggle)
            {
                Listing_Standard sectionTriggering = Main_List.BeginSection(100);
                sectionTriggering.Label("CR_SettingLabel_TriggeringContent".Translate());
                sectionTriggering.GapLine();
                sectionTriggering.CheckboxLabeled("CR_Setting_SelfHarm_ContentToggle".Translate(), ref settings.selfHarmContent_enabled, "CR_Setting_SelfHarm_ContentToggle_TT".Translate());
                sectionTriggering.CheckboxLabeled("CR_Setting_SelfHarm_AllXenotypesSelfHarm".Translate(), ref settings.selfHarmContent_allXenotypesSelfHarm, "CR_Setting_SelfHarm_AllXenotypesSelfHarm_TT".Translate());
                Main_List.EndSection(sectionTriggering);
                Main_List.Gap();
            }
                //
            Main_List.End();
            //
            base.DoSettingsWindowContents(inRect);
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

        public bool triggerContent_toggle = false;
        public bool selfHarmContent_enabled = false;
        public bool selfHarmContent_allXenotypesSelfHarm = false;
        public override void ExposeData()
        {
            // General Settings
            Scribe_Values.Look(ref triggerContent_toggle, "general_trigger_content_toggle", false);
            // The Company Settings
            Scribe_Values.Look(ref storytellerCompanySendsTraps, "storyteller_company_sends_traps", true);
            Scribe_Values.Look(ref storytellerSilentTrapDeployment, "storyteller_silent_trap_deployment", true);
            // Sound Cannon
            Scribe_Values.Look(ref soundCannon_ShockwaveDestroysLimbs, "soundcannon_shockwave_destroys_limbs", false);
            Scribe_Values.Look(ref soundCannon_UseLoudBlastEffect, "soundcannon_use_loud_blast_effect", false);
            // Triggering Content
            Scribe_Values.Look(ref selfHarmContent_enabled, "triggering_self_harm_content_enabled", false);
            Scribe_Values.Look(ref selfHarmContent_allXenotypesSelfHarm, "triggering_self_harm_all_xenotypes", false);
            //
            base.ExposeData();
        }
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