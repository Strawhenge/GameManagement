using Strawhenge.GameManagement.SaveRepository;
using System;

namespace Strawhenge.GameManagement.Unity.Saving
{
    class NullSaveGame : ISaveGame
    {
        public static ISaveGame Instance { get; } = new NullSaveGame();

        NullSaveGame()
        {
        }

        public event Action SaveStarted;

        public event Action SaveDataGenerated;

        public event Action SaveCompleted;

        public bool InProgress => false;

        public void Save(SaveMetaData saveToOverwrite = null)
        {
        }
    }
}

