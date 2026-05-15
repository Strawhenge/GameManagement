using System;

namespace Strawhenge.GameManagement.Unity
{
    public class RestartGame : IRestartGame
    {
        public event Action Restarting;

        public void Restart() => Restarting?.Invoke();
    }
}