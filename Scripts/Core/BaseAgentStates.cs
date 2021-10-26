using System;
using Objects;
using ScriptableObjects.Alignments;
using UnityEngine;

namespace Core
{
    public abstract class BaseAgentStates
    {
        protected Enemy agent;
        protected Transform target;
        protected Player player;
        
        
        protected float duration;
        
        public BaseAgentStates(Enemy agent)
        {
            this.agent = agent;
            player = Player.Instance;
            target = player.Parent;
        }
        
        public abstract Type Tick();

        public abstract Type Freeze();

        public abstract Type Burn();
        
        public abstract Type Die();

        public abstract Type TransformAgent();

        public virtual void TransformType(TransformAlignment alignment)
        {
            
        }

        public virtual void SetDuration(float seconds)
        {
            duration = seconds;
        }
    }
}