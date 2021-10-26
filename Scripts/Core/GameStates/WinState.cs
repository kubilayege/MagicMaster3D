using System;
using Managers;

namespace Core.GameStates
{
    public class WinState : IGameState
    {
        public WinState()
        {
            ActionManager.Instance.TriggerAction(ActionIDHolder.OnLevelCompletedID);
        }

        public IGameState Win()
        {
            return this;
        }

        public IGameState Lose()
        {
            return this;
        }

        public IGameState Play()
        {
            return new IdleState(LevelLoadType.Next);
        }

        public Type Type()
        {
            return typeof(WinState);
        }
    }
}