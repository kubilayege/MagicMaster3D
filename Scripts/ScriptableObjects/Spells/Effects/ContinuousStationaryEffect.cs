using System;
using System.Collections;
using Managers;
using Objects;
using UnityEngine;
using Utils;

namespace ScriptableObjects.Spells.Effects
{
    [CreateAssetMenu(menuName = "Create Spell Effect/Continuous Stationary Effect")]
    public class ContinuousStationaryEffect : Effect
    {
        public float damageTick = 0.2f;
        public float duration = 1f;
        public float effectDamage;
        public float radius;
        public GameObject effectParticlePrefab;
        private GameObject effectParticleInstance;
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
            var spellduration = duration;
            effectParticleInstance = Instantiate(effectParticlePrefab, CastPosition,
                effectParticlePrefab.transform.rotation, LevelManager.Instance.currentLevel.transform);
            effectParticleInstance.transform.localScale *= Type.GetEffectArea() * 2f;
            while (spellduration > 0)
            {
                Debug.Log("StationaryDamaging");
                var enemies = Type.GetEnemies(CastPosition);
                
                if(enemies.Count >= perfectCount)
                    OnPerfect(enemies.GetCenterPoint());
                
                spellduration -= damageTick;
                
                foreach (var enemy in enemies)
                {
                    if (alignment != null)
                        enemy.Status(alignment);
                    enemy.Damage(effectDamage);
                }
                yield return Wait.ForSeconds(damageTick);
            }

            // Destroy(effectParticleInstance);
            // Debug.Log("StationaryFinished");
            
            if (secondaryEffect != null)
            {
                var secondaryEffectInstance = Instantiate(secondaryEffect, Spell.transform);
                secondaryEffectInstance.Cast(Spell, Type, SpellCasterAgent, CastPosition);
            }
            
            DestroyEffect();
        }

        private void OnDestroy()
        {
            if(effectParticleInstance != null)
                DestroyImmediate(effectParticleInstance);
        }
    }
}