using System;
using System.Collections;
using Objects;
using UnityEngine;
using Utils;
using DG.Tweening;
using Managers;
using Random = UnityEngine.Random;

namespace ScriptableObjects.Spells.Effects
{
    [CreateAssetMenu(menuName = "Create Spell Effect/Momentary Delayed Projectile Effect")]
    public class MomentaryDelayedProjectileEffect : Effect
    {
        [Header("Particles")]
        public GameObject incomingEffectHitZoneIndicator;
        public GameObject incomingObject;
        public GameObject hitEffect;

        [Header("Properties")] 
        public float delayDuration;
        public float travelSpeedOfIncomingObject;
        public float effectDamage;


        private Vector3 _incomingObjectStartPosition;

        private GameObject _incomingHitZoneIndicatorInstance;
        private GameObject _incomingObjectInstance;
        private GameObject _hitEffectInstance;
        
        
        
        public override void Cast(Spell spell, SpellType type, Agent agent, Vector3 castPosition)
        {
            Spell = spell;
            Type = type;
            SpellCasterAgent = agent;
            CastPosition = castPosition;
            Spell.StartCoroutine(SpellEffect());
        }

        public override IEnumerator SpellEffect()
        {
            SpellCasterAgent.PlayAnimation(SpellCasterAgent.PlayerAnimCastAoe, true);
            
            yield return Wait.ForSeconds(castDuration);
            
            SpawnEffects(CalculateIncomingObjectProperties());
            
            
            //Delay
            yield return Wait.ForSeconds(delayDuration);
            
            ActivateEffect();

            if (secondaryEffect != null)
            {
                var secondaryEffectInstance = Instantiate(secondaryEffect, Spell.transform);
                secondaryEffectInstance.Cast(Spell, Type, SpellCasterAgent, CastPosition);
            }
        }

        private void ActivateEffect()
        {
            var enemies = Type.GetEnemies(CastPosition);
            if(enemies.Count >= perfectCount)
                OnPerfect(enemies.GetCenterPoint());
            foreach (var enemy in enemies)
            {
                enemy.Damage(effectDamage);
                
                if (alignment != null)
                    enemy.Status(alignment);
                
                var forceDirection = (enemy.ParentBody.position - CastPosition);
                enemy.ParentBody.AddForce((forceDirection / (forceDirection.magnitude / Type.GetEffectArea()) )* spellForce, ForceMode.Impulse);
            }
        }

        private IncomingObjectData CalculateIncomingObjectProperties()
        {
            var distanceToTravel = delayDuration * travelSpeedOfIncomingObject;
            var incomingObjectDirection =  new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
            
            var incomingObjectSpawnPos = CastPosition + (incomingObjectDirection * distanceToTravel);

            return new IncomingObjectData(-incomingObjectDirection, incomingObjectSpawnPos);
            
        }

        private void SpawnEffects(IncomingObjectData incomingObjectData)
        {
            _incomingObjectInstance =
                Spell.Instantiate(incomingObject, incomingObjectData.SpawnPos, incomingObject.transform.rotation,
                    LevelManager.Instance.currentLevel.transform);
            _incomingObjectInstance.transform.localScale *= Type.GetEffectArea()* 2f;
            
            if (incomingEffectHitZoneIndicator != null)
            {
                _incomingHitZoneIndicatorInstance =
                    Instantiate(incomingEffectHitZoneIndicator, CastPosition,
                        incomingEffectHitZoneIndicator.transform.rotation,
                        LevelManager.Instance.currentLevel.transform);
                
                _incomingHitZoneIndicatorInstance.transform.localScale *= Type.GetEffectArea()* 2f;
            };
         

            _incomingObjectInstance.transform.DOMove(CastPosition, delayDuration)
                .OnComplete(() =>
                    {
                        // if(_incomingHitZoneIndicatorInstance != null)
                        //     DestroyImmediate(_incomingHitZoneIndicatorInstance);
                        
                        if (hitEffect != null)
                        {
                            _hitEffectInstance = Instantiate(hitEffect, CastPosition, hitEffect.transform.rotation,
                                LevelManager.Instance.currentLevel.transform);
                            _hitEffectInstance.transform.localScale *= Type.GetEffectArea() * 2f;
                            // Destroy(_hitEffectInstance, 1f);
                        }
                        
                        // DestroyImmediate(_incomingObjectInstance);
                        
                        DestroyEffect();
                    }).SetEase(Ease.InFlash);

        }

        private void OnDestroy()
        {
            if(_incomingHitZoneIndicatorInstance != null)
                DestroyImmediate(_incomingHitZoneIndicatorInstance);
            if(_incomingObjectInstance != null)
                DestroyImmediate(_incomingObjectInstance);
            if(_hitEffectInstance != null)
                Destroy(_hitEffectInstance,1f);
        }
    }
}