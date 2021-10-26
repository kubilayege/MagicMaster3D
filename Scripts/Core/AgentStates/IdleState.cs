using System;
using Objects;
using UnityEngine;
using Utils;

namespace Core.AgentStates
{
    public class IdleState : BaseAgentStates
    {
        public IdleState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            Debug.Log($"IdleState =>  {CheckPlayerInRange()}");
            if (CheckPlayerInRange())
                return typeof(AgroState);

            agent.PlayAnimation(agent.EnemyAnimIdle, false);
            
            return typeof(IdleState);
        }

        private bool CheckPlayerInRange()
        {
            if (target.position.Distance(agent.Parent.position) <= agent.EnemyData.AgroRange)
                return true;

            return false;
        }
        

        public override Type Freeze()
        {
            return typeof(FreezeState);
        }

        public override Type Burn()
        {
            return typeof(BurnState);
        }

        public override Type Die()
        {
            return typeof(RagdollState);
        }

        public override Type TransformAgent()
        {
            return typeof(TransformState);
        }
    }
}