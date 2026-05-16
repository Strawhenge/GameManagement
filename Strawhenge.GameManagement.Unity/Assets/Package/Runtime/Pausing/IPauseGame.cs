using System;

namespace Strawhenge.GameManagement.Unity.Pausing
{
    public interface IPauseGame
    {
        event Action Paused;

        event Action Resumed;

        bool IsPaused { get; }

        void Pause();

        void Resume();
    }
}

