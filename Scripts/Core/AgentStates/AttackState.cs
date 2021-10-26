using System;
using System.Collections;
using Objects;
using Utils;

namespace Core.AgentStates
{
    public class AttackState : BaseAgentStates
    {
        private bool _isAttacking;
        public AttackState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            if(!CheckPlayerInRange())
            {
                agent.StopCoroutine(Attack());
                return typeof(IdleState);
            }

            if (!PlayerInAttackRange())
            {
                agent.StopCoroutine(Attack());
                return typeof(AgroState);
            }

            if (!_isAttacking)
                agent.StartCoroutine(Attack());


            return typeof(AttackState);
        }

        private IEnumerator Attack()
        {
            _isAttacking = true;
            
            agent.transform.LookAt(target.position.WithY(agent.transform.position.y));
            agent.PlayAnimation(agent.EnemyAnimHit, true);
            player.Damage(agent.EnemyData.AttackDamage);
            yield return Wait.ForSeconds(agent.EnemyData.AttackSpeed);
            
            _isAttacking = false;
        }

        private bool CheckPlayerInRange()
        {
            if (target.position.Distance(agent.Parent.position) <= agent.EnemyData.AgroRange)
                return true;

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
            _isAttacking = false;
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