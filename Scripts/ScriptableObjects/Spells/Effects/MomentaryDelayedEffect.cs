using System;
using System.Collections;
using Managers;
using Objects;
using UnityEngine;
using Utils;

namespace ScriptableObjects.Spells.Effects
{
    [CreateAssetMenu(menuName = "Create Spell Effect/Momentary Delayed Effect")]
    public class MomentaryDelayedEffect : Effect
    {
        public float delayDuration = 0f;
        
        public float effectDamage;

        public GameObject hitEffect;
        private GameObject hitEffectInstance;
        
        public override void Cast(Spell spell, SpellType type, Agent agent, Vector3 castPosition)
        {
            Spell = spell;
            Type = type;
            SpellCasterAgent = agent;
            CastPosition = castPosition;
            spell.StartCoroutine(SpellEffect());
        }

        public override IEnumerator SpellEffect()
        {
            SpellCasterAgent.PlayAnimation(SpellCasterAgent.PlayerAnimCastProjectile, true);
            
            yield return Wait.ForSeconds(castDuration);
            
            yield return Wait.ForSeconds(delayDuration-castDuration);
            hitEffectInstance = Instantiate(hitEffect, CastPosition, hitEffect.transform.rotation,
                LevelManager.Instance.currentLevel.transform);
            var enemies = Type.GetEnemies(CastPosition);
            
            if(enemies.Count >= perfectCount)
                OnPerfect(enemies.GetCenterPoint());
            
            foreach (var enemy in enemies)
            {
                enemy.Damage(effectDamage);
                
                if(alignment != null)
                    enemy.Status(alignment);

                var forceDirection = (enemy.ParentBody.position - CastPosition);
                enemy.ParentBody.AddForce((forceDirection / (forceDirection.magnitude / Type.GetEffectArea()) )* spellForce);
            }
            
            if (secondaryEffect != null)
            {
                var secondaryEffectInstance = Instantiate(secondaryEffect, Spell.transform);
                secondaryEffectInstance.Cast(Spell, Type, SpellCasterAgent, CastPosition);
            }
            
            DestroyEffect();
            
        }

        private void OnDestroy()
        {
            if(hitEffectInstance != null)
                Destroy(hitEffectInstance, 2f);
        }
    }
}