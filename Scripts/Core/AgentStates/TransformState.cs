using System;
using Objects;
using ScriptableObjects;
using ScriptableObjects.Alignments;
using UnityEngine;

namespace Core.AgentStates
{
    public class TransformState : BaseAgentStates
    {
        private Alignment _alignment;
        
        public TransformState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            duration -= Time.deltaTime;
            
            if (duration > 0)
                return typeof(TransformState);

            StopEffect();
            return typeof(IdleState);
            
        }

        private void StopEffect()
        {
            agent.TransformIntoDefaultModel();
            agent.NavMeshAgent.isStopped = false;
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

        public override void TransformType(TransformAlignment alignment)
        {
            _alignment = alignment;
            SetDuration(alignment.duration);
            agent.TransformInto(alignment.transformedObject);
            agent.PlayAnimation(agent.EnemyAnimIdle, true);
        }
    }
}