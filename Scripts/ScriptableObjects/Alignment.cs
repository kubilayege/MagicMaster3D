using Core;
using Objects;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class Alignment : ScriptableObject
    {
        public float duration;
        public abstract AlignmentData Effect(Agent agent);
        public abstract void Tick(Agent agent);
    }
}