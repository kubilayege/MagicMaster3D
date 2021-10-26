using System;
using Objects;
using UnityEngine;
using Utils;

namespace Core.AgentStates
{
    public class AgroState : BaseAgentStates
    {
        public AgroState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            if (PlayerInAttackRange())
                return typeof(AttackState);
            
            if (!CheckPlayerInRange())
                return typeof(IdleState);

            MoveAgentToAttackRange(target.position, agent.Parent.position);
            
            agent.PlayAnimation(agent.EnemyAnimRun, true);
            
            return typeof(AgroState);
        }

        private void MoveAgentToAttackRange(Vector3 targetPos, Vector3 agentPos)
        {
            var betweenVector = targetPos.Minus(agentPos).WithY(0f);
            var direction = betweenVector.normalized;
            Debug.Log("Moving");
            agent.transform.LookAt(targetPos.WithY(agentPos.y));
            agent.NavMeshAgent.destination = targetPos;
            agent.NavMeshAgent.stoppingDistance = agent.EnemyData.AttackRange;
            agent.NavMeshAgent.speed = agent.EnemyData.MoveSpeed;
        }

        private bool CheckPlayerInRange()
        {
            if (target.position.Distance(agent.Parent.position) <= agent.EnemyData.AgroRange)
                return true;

            agent.NavMeshAgent.destination = agent.Parent.position;
            return false;
        }

        
        private bool PlayerInAttackRange()
        {
            if (target.position.Distance(agent.Parent.position) <= agent.EnemyData.AttackRange)
                return true;

            return false;
        }

        private void StopAttacking()
        {
            agent.NavMeshAgent.destination = agent.Parent.position;
            agent.NavMeshAgent.isStopped = true;
        }
        
        public override Type Freeze()
        {
            StopAttacking();
            return typeof(FreezeState);
        }

        public override Type Burn()
        {
            StopAttacking();
            return typeof(BurnState);
        }

        public override Type Die()
        {
            StopAttacking();
            return typeof(RagdollState);
        }

        public override Type TransformAgent()
        {
            StopAttacking();
            return typeof(TransformState);
        }
    }
}