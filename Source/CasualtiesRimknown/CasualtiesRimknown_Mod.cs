using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using HarmonyLib;

namespace CasualtiesRimknown
{
    public class CasualtiesRimknown_Mod : Mod
    {
        public CasualtiesRimknown_Mod(ModContentPack content) : base(content)
        {
            new Harmony(base.Content.PackageIdPlayerFacing).PatchAll();
        }
    }
}
