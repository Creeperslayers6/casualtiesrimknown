using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace CasualtiesRimknown.JobGivers
{
    public class JobGiver_SelfharmEvent : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            float pawnMoodLevel = pawn.needs.mood.CurLevelPercentage;
            if (pawnMoodLevel <= 0.15f)
            {
                // Critical Event (Ranged Verb Onself OR Five Slashes OR Destroy Eye)

                // CHECK FOR RANGED WEAPON
                //Verb primaryAttackVerb = pawn.TryGetAttackVerb(pawn);
                //if (typeof(Verb_Shoot).IsAssignableFrom(primaryAttackVerb.verbProps.verbClass))
                //{
                //    return JobMaker.MakeJob(JobDefOf.UseVerbOnThing, pawn);
                //}

                IEnumerable<BodyPartRecord> validEyePartList = pawn.health.hediffSet.GetNotMissingParts().Where((BodyPartRecord p) => p.def == BodyPartDefOf.Eye);
                if (validEyePartList.Count() == 0)
                {
                    Log.Message("No Valid Eyeballs Found!!!");
                    return JobMaker.MakeJob(DefOfs.JobDefOf.CR_SelfharmSlashTerminal, pawn);
                }
                else
                {
                    int destroyEyeChance = Rand.Range(1, 5);
                    if (destroyEyeChance == 1)
                    {
                        // Destroy an Eye!
                        return JobMaker.MakeJob(DefOfs.JobDefOf.CR_SelfharmSlashEye, pawn);
                    }
                    else
                    {
                        // Five Slashes!
                        return JobMaker.MakeJob(DefOfs.JobDefOf.CR_SelfharmSlashTerminal, pawn);
                    }
                }
            }
            else
            {
                // Bad Event (Single Slash)
                return JobMaker.MakeJob(DefOfs.JobDefOf.CR_SelfharmSlashOnce, pawn);
            }
        }
    }
}
