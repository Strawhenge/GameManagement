using System;

namespace Strawhenge.GameManagement.Unity
{
    class NullRestartGame : IRestartGame
    {
        public static IRestartGame Instance { get; } = new NullRestartGame();

        NullRestartGame()
        {
        }

        public event Action Restarting;

        public void Restart()
        {
        }
    }
}

