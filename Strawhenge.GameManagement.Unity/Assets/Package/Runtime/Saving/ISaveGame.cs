using System;

namespace Strawhenge.GameManagement.Unity
{
    public interface ISaveGame
    {
        event Action SaveStarted;

        event Action SaveDataGenerated;

        event Action SaveCompleted;

        bool InProgress { get; }

        void Save(SaveMetaData saveToOverwrite = null);
    }
}

