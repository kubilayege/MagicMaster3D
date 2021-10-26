using System;
using System.Collections;
using Objects;
using Utils;

namespace Core.AgentStates
{
    public class BurnState : BaseAgentStates
    {
        public BurnState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            if (duration <= 0)
            {
                return typeof(IdleState);
            }
            return typeof(BurnState);
        }

        public override Type Freeze()
        {
            StopBurning();
            return typeof(FreezeState);
        }

        public override Type Burn()
        {
            StopBurning();
            return typeof(BurnState);
        }

        public override Type Die()
        {
            StopBurning();
            return typeof(RagdollState);
        }

        public override Type TransformAgent()
        {
            StopBurning();
            return typeof(TransformState);
        }

        public override void SetDuration(float seconds)
        {
            base.SetDuration(seconds);
            agent.StartCoroutine(BurnRoutine());
        }

        private void StopBurning()
        {
            agent._currentAlignment.DeActivateEffect();
            agent.StopCoroutine(BurnRoutine());
        }

        private IEnumerator BurnRoutine()
        {
            yield return Wait.EndOfFrame;
            agent._currentAlignment.ActivateEffect();
            while (duration > 0)
            {
                agent._currentAlignment.Alignment.Tick(agent);
                yield return Wait.ForSeconds(agent._currentAlignment.Rate);
                duration -= agent._currentAlignment.Rate;
            }
            StopBurning();
        }
    }
}