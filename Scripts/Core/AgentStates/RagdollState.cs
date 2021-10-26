using System;
using System.Collections;
using Objects;
using Utils;
using Object = UnityEngine.Object;

namespace Core.AgentStates
{
    public class RagdollState : BaseAgentStates
    {
        private bool isRagdoll;
        public RagdollState(Enemy agent) : base(agent)
        {
        }

        public override Type Tick()
        {
            if (agent.ParentBody.position.y < -15f)
                Object.Destroy(agent.gameObject);
            if(!isRagdoll)
                agent.StartCoroutine(Ragdoll());

            return typeof(RagdollState);
        }

        private IEnumerator Ragdoll()
        {
            isRagdoll = true;

            agent.DisableAnimator();
            
            yield return Wait.ForSeconds(duration);
            
            //
            // // Object.Destroy(agent.gameObject);
            // isRagdoll = false;
        }

        public override Type Freeze()
        {
            return typeof(RagdollState);
        }

        public override Type Burn()
        {
            return typeof(RagdollState);
        }

        public override Type Die()
        {
            return typeof(RagdollState);
        }

        public override void SetDuration(float seconds)
        {
            duration = seconds;
        }

        public override Type TransformAgent()
        {
            return typeof(RagdollState);
        }
    }
}