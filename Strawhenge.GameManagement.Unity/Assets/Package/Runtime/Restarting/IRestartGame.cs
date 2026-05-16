using System;

namespace Strawhenge.GameManagement.Unity.Restarting
{
    public interface IRestartGame
    {
        event Action Restarting;

        void Restart();
    }
}

