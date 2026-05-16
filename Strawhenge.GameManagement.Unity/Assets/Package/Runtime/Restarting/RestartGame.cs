using System;

namespace Strawhenge.GameManagement.Unity.Restarting
{
    public class RestartGame : IRestartGame
    {
        public event Action Restarting;

        public void Restart() => Restarting?.Invoke();
    }
}