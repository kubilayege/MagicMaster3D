using Core;
using Objects;
using UnityEngine;

namespace ScriptableObjects.Alignments
{
    [CreateAssetMenu(menuName = "Create Alignment/Transform Alignment")]
    public class TransformAlignment : Alignment
    {
        public GameObject transformedObject;

        public override AlignmentData Effect(Agent agent)
        {
            agent.stateMachine.TransformInto(this);
            return new AlignmentData(this, null, agent, duration, 0f);
        }

        public override void Tick(Agent agent)
        {
            return;
        }
    }
}