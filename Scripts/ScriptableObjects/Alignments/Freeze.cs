using System.Collections.Generic;
using Core;
using Objects;
using UnityEngine;

namespace ScriptableObjects.Alignments
{
    [CreateAssetMenu(menuName = "Create Alignment/Freeze")]
    public class Freeze : Alignment
    {
        public GameObject freezeEffect;
        
        public override AlignmentData Effect(Agent agent)
        {
            var alignmentData = new AlignmentData(this, freezeEffect, agent, duration, 0f);
            agent.stateMachine.Freeze(duration);
            return alignmentData;
        }

        public override void Tick(Agent agent)
        {
            return;
        }
    }
}