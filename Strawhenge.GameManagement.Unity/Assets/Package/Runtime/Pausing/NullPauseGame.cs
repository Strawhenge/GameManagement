using System;

namespace Strawhenge.GameManagement.Unity.Pausing
{
    class NullPauseGame : IPauseGame
    {
        public static IPauseGame Instance { get; } = new NullPauseGame();

        NullPauseGame()
        {
        }

        public event Action Paused;

        public event Action Resumed;

        public bool IsPaused => false;

        public void Pause()
        {
        }

        public void Resume()
        {
        }
    }
}

