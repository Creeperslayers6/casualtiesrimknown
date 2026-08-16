using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.IncidentWorkers
{
    public class IncidentWorker_SoundCannonDrop : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            return CasualtiesRimknown_Mod.settings.storytellerCompanySendsTraps;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (Find.FactionManager.FirstFactionOfDef(DefOfs.FactionDefOf.CR_TheCompany) == null)
            {
                return false;
            }
            if (!TryFindSoundCannonDropCell(map.Center, map, 999999, out var pos))
            {
                return false;
            }
            //
            Thing newSoundCannon = ThingMaker.MakeThing(DefOfs.ThingDefOf.CR_Turret_SoundCannonBasic, ThingDefOf.Steel);
            Faction soundCannonFaction = Find.FactionManager.FirstFactionOfDef(DefOfs.FactionDefOf.CR_TheCompany);
            if (newSoundCannon.def.CanHaveFaction)
            {
                newSoundCannon.SetFactionDirect(soundCannonFaction);
            }
            SkyfallerMaker.SpawnSkyfaller(DefOfs.ThingDefOf.CR_Skyfaller_SoundCannonIncoming, newSoundCannon, pos, map);
            // 
            if (!CasualtiesRimknown_Mod.settings.storytellerSilentTrapDeployment)
            {
                Messages.Message("CR_CompanySentTrapWarningMessage".Translate(soundCannonFaction), new TargetInfo(pos, map), MessageTypeDefOf.NeutralEvent);
            }
            //
            return true;
        }

        private bool TryFindSoundCannonDropCell(IntVec3 nearLOC, Map map, int maxDist, out IntVec3 pos)
        {
            return CellFinderLoose.TryFindSkyfallerCell(DefOfs.ThingDefOf.CR_Skyfaller_SoundCannonIncoming, map, DefOfs.ThingDefOf.CR_Turret_SoundCannonBasic.terrainAffordanceNeeded, out pos, 10, nearLOC, maxDist);
        }
    }
}
