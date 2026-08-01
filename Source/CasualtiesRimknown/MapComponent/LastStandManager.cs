using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace CasualtiesRimknown
{
    public class LastStandManager(Map map) : MapComponent(map)
    {
        public List<Corpse> lastStandCapableCorpses = new List<Corpse>();

        public void Register(Corpse newCorpse)
        {
            if (newCorpse?.InnerPawn.genes.HasActiveGene(DefOfs.GeneDefOf.CR_LastStand) == true)
            {
                lastStandCapableCorpses.Add(newCorpse);
            }
        }

        public void Deregister(Corpse oldCorpse)
        {
            Log.Message("Requested Remove" + oldCorpse.ThingID);
            lastStandCapableCorpses.Remove(oldCorpse);
        }

        public override void MapComponentTick()
        {
            if (Find.TickManager.TicksGame % 30 == 0)
            {
                for (int i = lastStandCapableCorpses.Count - 1; i >= 0; i--)
                {
                    // Catch Null Entries!
                    if (lastStandCapableCorpses[i] == null || lastStandCapableCorpses[i].InnerPawn == null)
                    {
                        lastStandCapableCorpses.Remove(lastStandCapableCorpses[i]);
                        continue;
                    }
                    Gene_LastStand firstGene = (Gene_LastStand)lastStandCapableCorpses[i].InnerPawn.genes.GetGene(DefOfs.GeneDefOf.CR_LastStand);
                    firstGene.TickRare();
                }
            }
        }
    }
}
