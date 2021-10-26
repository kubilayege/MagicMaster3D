using System;
using Managers;
using UnityEngine;

namespace Core.GameStates
{
    public class IdleState : IGameState
    {
        public IdleState(LevelLoadType nextLevelLoadType)
        {
            LevelManager.Instance.InitLevel(nextLevelLoadType);
            ActionManager.Instance.TriggerAction(ActionIDHolder.OnLevelPreparedID);
        }

        public IGameState Win()
        {
            Debug.Log("Idle => Win");
            return new WinState();
        }

        public IGameState Lose()
        {
            Debug.Log("Idle => Lose");
            return new LoseState();
        }

        public IGameState Play()
        {
            return new PlayState();
        }

        public Type Type()
        {
            return typeof(IdleState);
        }
    }
}