using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using Managers;
using Objects;
using ScriptableObjects.Spells.SpellTypes;
using UnityEngine;
using Utils;

namespace ScriptableObjects.Spells.Effects
{
    [CreateAssetMenu(menuName = "Create Spell Effect/Continuous Moving Effect")]
    public class ContinuousMovingProjectileEffect : Effect
    {
        public bool StopOnFirstHit;
        public GameObject movingObject;
        public GameObject hitEffect;
        public float speed;
        public float effectDamage;
        
        private Projectile _spellType;

        private GameObject _movingObjectInstance;
        private GameObject _hitEffectInstance;
        private Vector3 castStartPosition;
        public override void Cast(Spell spell, SpellType type, Agent agent, Vector3 castPosition)
        {
            _spellType = (Projectile) type;
            Spell = spell;
            SpellCasterAgent = agent;
            CastPosition = castPosition;
            castStartPosition = SpellCasterAgent.Parent.position;
            spell.StartCoroutine(SpellEffect());
        }

        public override IEnumerator SpellEffect()
        {
            SpellCasterAgent.PlayAnimation(SpellCasterAgent.PlayerAnimCastProjectile, true);
            var direction = CastPosition.WithY(0) - SpellCasterAgent.Parent.position.WithY(0);
            yield return Wait.ForSeconds(castDuration);
            _movingObjectInstance = Instantiate(movingObject, SpellCasterAgent.SpellCastOrigin.position,
                Quaternion.identity, LevelManager.Instance.currentLevel.transform);

            var endPos = _movingObjectInstance.transform.position +
                         (direction).normalized * _spellType.range;

            _movingObjectInstance.transform.localScale *= _spellType.width;   
            
            _movingObjectInstance.transform
                .DOMove(endPos, _spellType.range / speed)
                .SetEase(Ease.Linear)
                .SetId(GetInstanceID())
                .OnUpdate((() =>
                {
                    var enemies = _spellType.GetEnemies(_movingObjectInstance.transform.position);
                    if(enemies.Count >= perfectCount)
                        OnPerfect(enemies.GetCenterPoint());
                    if (enemies.Count > 0)
                    {
                        if (StopOnFirstHit && !(enemies.Any((enemy => enemy._currentHealth>0))))
                        {
                            return;
                        }
                        
                        foreach (var enemy in enemies)
                        {
                            
                            Debug.Log("VAR");
                            enemy.Damage(effectDamage);
                            if (alignment != null)
                                enemy.Status(alignment);

                            if (hitEffect != null)
                            {
                                _hitEffectInstance = Instantiate(hitEffect, enemy.ParentBody.position,
                                        hitEffect.transform.rotation, LevelManager.Instance.currentLevel.transform);
                            }
                            
                            var forceDirection = (endPos - castStartPosition).normalized;
                            enemy.ParentBody.AddForce(forceDirection * spellForce, ForceMode.Impulse);
                        }
                        
                        if (StopOnFirstHit)
                            StopEffect();
                        
                    }
                }));

            yield return Wait.ForSeconds(_spellType.range / speed);
            if (secondaryEffect != null)
            {
                var secondaryEffectInstance = Instantiate(secondaryEffect, Spell.transform);
                secondaryEffectInstance.Cast(Spell, Type, SpellCasterAgent, CastPosition);
            }
            if(_movingObjectInstance != null)
                StopEffect();
        }

        private void StopEffect()
        {
            DOTween.Kill(GetInstanceID());
            // Destroy(_movingObjectInstance);
            DestroyEffect();
        }

        private void OnDestroy()
        {
            if(_movingObjectInstance != null)
                DestroyImmediate(_movingObjectInstance);
            if(_hitEffectInstance != null)
                DestroyImmediate(_hitEffectInstance);
        }
    }
}