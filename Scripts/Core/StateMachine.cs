using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Alignments;
using UnityEngine;

namespace Core
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, BaseAgentStates> states;
        public BaseAgentStates CurrentState { get; private set; }

        public event Action<BaseAgentStates> OnStateChanged;

        public void SetStates(Dictionary<Type, BaseAgentStates> initialStates)
        {
            this.states = initialStates;
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                CurrentState = states.Values.First();
            }

            var nextState = CurrentState?.Tick();

            SwitchState(nextState);
        }
    
        protected virtual void SwitchState(Type nextState)
        {
            if (!(nextState != null && nextState != CurrentState?.GetType()))
                return;
            
            CurrentState = states[nextState];
            OnStateChanged?.Invoke(CurrentState);
        }

        public void Freeze(float seconds)
        {
            var nextState = CurrentState.Freeze();
            SwitchState(nextState);
            CurrentState.SetDuration(seconds);
        }
        
        public void Burn(float seconds)
        {
            var nextState = CurrentState.Burn();
            SwitchState(nextState);
            CurrentState.SetDuration(seconds);
        }

        public void Die()
        {
            var nextState = CurrentState.Die();
            SwitchState(nextState);
            CurrentState.SetDuration(2f);
        }

        public void TransformInto(TransformAlignment alignment)
        {
            var nextState = CurrentState.TransformAgent();
            SwitchState(nextState);
            CurrentState.TransformType(alignment);
        }
    }
}