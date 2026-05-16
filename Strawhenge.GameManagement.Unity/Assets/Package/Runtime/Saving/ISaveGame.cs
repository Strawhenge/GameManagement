using Strawhenge.GameManagement.SaveRepository;
using System;

namespace Strawhenge.GameManagement.Unity.Saving
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

