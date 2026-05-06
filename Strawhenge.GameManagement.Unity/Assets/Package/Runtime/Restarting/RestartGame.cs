using System;

namespace Strawhenge.GameManagement.Unity
{
    public class RestartGame
    {
        public event Action Restarting;

        public void Restart() => Restarting?.Invoke();
    }
}