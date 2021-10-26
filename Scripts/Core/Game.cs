using System;
using Core.GameStates;

namespace Core
{
    [Serializable]
    public class Game
    {
        public IGameState currentGameState;

        public Game(IGameState initialState)
        {
            currentGameState = initialState;
        }
        
        public void Play()
        {
            currentGameState = currentGameState.Play();
        }

        public void Win()
        {
            currentGameState = currentGameState.Win();
        }

        public void Lose()
        {
            currentGameState = currentGameState.Lose();
        }

        public Type GetCurrentState()
        {
            return currentGameState.Type();
        }
    }
}