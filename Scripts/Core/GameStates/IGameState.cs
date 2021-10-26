using System;

namespace Core.GameStates
{
    public interface IGameState
    {
        IGameState Win();
        IGameState Lose();
        IGameState Play();

        Type Type();
    }
}