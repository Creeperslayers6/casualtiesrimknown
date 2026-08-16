using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CasualtiesRimknown.Verbs
{
    // Based on Verb_SpewFire : Verb
    public class Verb_ArcShockwave : Verb
    {
        float firingAngle = 70f;
        protected override bool TryCastShot()
        {
            if (currentTarget.HasThing && currentTarget.Thing.Map != caster.Map)
            {
                return false;
            }
            if (base.EquipmentSource != null)
            {
                base.EquipmentSource.GetComp<CompChangeableProjectile>()?.Notify_ProjectileLaunched();
                base.EquipmentSource.GetComp<CompApparelReloadable>()?.UsedOnce();
            }
            IntVec3 position = caster.Position;
            // Firing Arc Calculations
            float targetAngle = Mathf.Atan2(-(currentTarget.Cell.z - position.z), currentTarget.Cell.x - position.x) * 57.29578f;
            FloatRange affectedAngle = new FloatRange(targetAngle - (firingAngle / 2), targetAngle + (firingAngle / 2));
            //
            IntVec3 center = position;
            Map mapHeld = caster.MapHeld;
            DamageDef damageDef = DefOfs.DamageDefOf.CR_SonicShockwave;
            Thing instigator = caster;
            ThingDef weapon = base.EquipmentSource.def;
            float screenShakeFactor = 3f;
            SoundDef selectedSoundDef = DefOfs.SoundDefOf.CR_SoundCannon_Blast;

            GenExplosion.DoExplosion(center, mapHeld, EffectiveRange, damageDef, instigator, damageDef.defaultDamage, damageDef.defaultArmorPenetration, selectedSoundDef, weapon, null, currentTarget.Thing, null, 0f, 1, null, null, 255, applyDamageToExplosionCellsNeighbors: false, null, 0f, 1, 0f, damageFalloff: true, null, null, affectedAngle, doVisualEffects: true, damageDef.expolosionPropagationSpeed, 0f, doSoundEffects: true, null, screenShakeFactor);
            return true;
        }
    }
}
