using System;

namespace Strawhenge.GameManagement.Unity
{
    public interface IRestartGame
    {
        event Action Restarting;

        void Restart();
    }
}

