using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CasualtiesRimknown.DamageWorkers
{
    public class DamageWorker_SonicShockwave : DamageWorker_AddInjury
    {
        private const float ExplosionCamShakeMultiplier = 4f;
        float stunDuration = 6f;

        // Intercept DamageWorker_AddInjury.Apply() to apply Stun effect on Pawns
        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {
            DamageResult damageResult = base.Apply(dinfo, thing);

            if (thing is Pawn pawn)
            {
                stunPawn(pawn, dinfo);
            }
            
            return damageResult;
        }

        private void stunPawn(Pawn pawn, DamageInfo dinfo)
        {
            pawn?.stances?.stunner?.StunFor(stunDuration.SecondsToTicks(), dinfo.Instigator, addBattleLog: false, showMote: true);
        }

        protected override BodyPartRecord ChooseHitPart(DamageInfo dinfo, Pawn pawn)
        {
            return pawn.health.hediffSet.GetRandomNotMissingPart(dinfo.Def, dinfo.Height, BodyPartDepth.Outside);
        }

        // Removed ExplosionVisualEffectCenter(explosion); from ExplosionStart
        public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
        {
            if (def.explosionHeatEnergyPerCell > float.Epsilon)
            {
                GenTemperature.PushHeat(explosion.Position, explosion.Map, def.explosionHeatEnergyPerCell * (float)cellsToAffect.Count);
            }
            if (explosion.doVisualEffects)
            {
                FleckMaker.Static(explosion.Position, explosion.Map, FleckDefOf.ExplosionFlash, explosion.radius * 6f);
                if (explosion.Map == Find.CurrentMap)
                {
                    float magnitude = (explosion.Position.ToVector3Shifted() - Find.Camera.transform.position).magnitude;
                    Find.CameraDriver.shaker.DoShake(ExplosionCamShakeMultiplier * explosion.radius * explosion.screenShakeFactor / magnitude);
                }
            }
        }
    }
}
