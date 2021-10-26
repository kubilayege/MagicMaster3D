using System;
using Managers;
using UnityEngine;

namespace Core.GameStates
{
    public class LoseState : IGameState
    {
        public LoseState()
        {
            ActionManager.Instance.TriggerAction(ActionIDHolder.OnLevelFailedID);
        }
        
        public IGameState Win()
        {
            Debug.Log("After lose state Win state triggered");
            return this;
        }

        public IGameState Lose()
        {
            Debug.Log("Lose State triggered again");
            return this;
        }

        public IGameState Play()
        {
            return new IdleState(LevelLoadType.Restart);
        }

        public Type Type()
        {
            return typeof(LoseState);
        }
    }
}