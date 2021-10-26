using Core;
using Objects;
using UnityEngine;

namespace ScriptableObjects.Alignments
{
    [CreateAssetMenu(menuName = "Create Alignment/Burn")]
    public class Burn : Alignment
    {
        public GameObject burnEffect;
        public float burnTickDamage;
        public float burnTickRate;
        
        public override AlignmentData Effect(Agent agent)
        {
            agent.stateMachine.Burn(duration);
            return new AlignmentData(this, burnEffect, agent, duration, burnTickRate);
        }

        public override void Tick(Agent agent)
        {
            agent.Damage(burnTickDamage);
        }
    }
}