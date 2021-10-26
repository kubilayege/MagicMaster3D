using System;
using DG.Tweening;
using Objects;
using UnityEngine;

namespace Core.AgentStates
{
    public class FreezeState : BaseAgentStates
    {
        private bool frozen;
        private AlignmentData freezeAlignment;
        public FreezeState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            ControlEffect();
            duration -= Time.deltaTime;
            
            if (duration > 0)
                return typeof(FreezeState);

            StopEffect();
            return typeof(IdleState);
        }

        private void StopEffect()
        {
            agent.ResumeAnimation();
            freezeAlignment.DeActivateEffect();
            agent._meshRenderer.material.DOColor(Color.white, 0.2f);
            foreach (var limb in agent.limbs)
            {
                limb.isKinematic = false;
            }
            
            agent.NavMeshAgent.isStopped = false;
            frozen = false;
        }

        private void ControlEffect()
        {
            if (frozen)
                return;
            
            frozen = true;
            freezeAlignment = new AlignmentData(agent._currentAlignment.Alignment,
                agent._currentAlignment.AlignmentEffectPrefab, agent._currentAlignment.AgentEffected, agent._currentAlignment.Duration,agent._currentAlignment.Rate );
            freezeAlignment.ActivateEffect();
            
            agent._meshRenderer.material.DOColor(Color.white, 0.1f).SetLoops(1, LoopType.Yoyo).SetEase(Ease.Flash).OnComplete(
                () =>
                {
                    agent._meshRenderer.material.color = Color.white;
                });
                
            agent._meshRenderer.materials[1].DOColor(Color.white, 0.1f).SetLoops(1, LoopType.Yoyo).SetEase(Ease.Flash).OnComplete(
                () =>
                {
                    agent._meshRenderer.materials[1].color = Color.white;
                });
            
            
            foreach (var limb in agent.limbs)
            {
                limb.isKinematic = true;
            }
            
            agent.PauseAnimation();
        }

        public override Type Freeze()
        {
            StopEffect();
            return typeof(FreezeState);
        }

        public override Type Burn()
        {
            StopEffect();
            return typeof(BurnState);
        }

        public override Type Die()
        {
            StopEffect();
            return typeof(RagdollState);
        }
        
        public override Type TransformAgent()
        {
            StopEffect();
            return typeof(TransformState);
        }
        
        public override void SetDuration(float seconds)
        {
            base.SetDuration(seconds);
            
            agent.ParentBody.isKinematic = false;
            frozen = false;
        }
        
    }
}