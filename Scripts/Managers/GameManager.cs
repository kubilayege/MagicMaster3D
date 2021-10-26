using System;
using Core;
using Core.GameStates;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Game game;

        private void Start()
        {
            game = new Game(new IdleState(LevelLoadType.Restart));
        }

        public void Play()
        {
            game.Play();
        }

        public void Lose()
        {
            game.Lose();
        }

        public void Win()
        {
            game.Win();
        }

        public bool IsPlaying()
        {
            return typeof(PlayState) == game.GetCurrentState();
        }
    }
}