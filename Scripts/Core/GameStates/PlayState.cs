using System;
using Managers;
using UnityEngine;

namespace Core.GameStates
{
    public class PlayState : IGameState
    {
        public PlayState()
        {
            ActionManager.Instance.TriggerAction(ActionIDHolder.OnLevelStartedID);
            Debug.Log("PlayState");
        }

        public IGameState Win()
        {
            Debug.Log("PlayState => WinState");
            return new WinState();
        }

        public IGameState Lose()
        {
            return new LoseState();
        }

        public IGameState Play()
        {
            return this;
        }

        public Type Type()
        {
            return typeof(PlayState);
        }
    }
}