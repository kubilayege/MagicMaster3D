using Managers;
using Objects;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class AlignmentData
    {
        public Alignment Alignment;
        public GameObject AlignmentEffectInstance;
        public Agent AgentEffected;
        public GameObject AlignmentEffectPrefab;
        public float Duration;
        public float Rate;
        
        

        public AlignmentData(Alignment alignment, GameObject alignmentEffect, Agent agentEffected, float duration, float rate)
        {
            Duration = duration;
            Rate = rate;
            Alignment = alignment;
            AgentEffected = agentEffected;
            AlignmentEffectPrefab = alignmentEffect;
        }

        public void ActivateEffect()
        {
            AlignmentEffectInstance = Object.Instantiate(AlignmentEffectPrefab, AgentEffected.ParentBody.position,
                AlignmentEffectPrefab.transform.rotation, LevelManager.Instance.currentLevel.transform);
        }
        
        public void DeActivateEffect()
        {
            Object.Destroy(AlignmentEffectInstance);
        }
    }
}