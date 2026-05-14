using Strawhenge.Common.Logging;
using Strawhenge.GameManagement.Saving;
using System;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveGame
    {
        readonly SavingScript _saving;
        readonly ISaveGameCommandFactory _commandFactory;
        readonly ILogger _logger;

        public SaveGame(SavingScript saving, ISaveGameCommandFactory commandFactory, ILogger logger)
        {
            _saving = saving;
            _commandFactory = commandFactory;
            _logger = logger;
        }

        public event Action SaveStarted;

        public event Action SaveSafeToReturnToGameplay; // TODO Rename this.

        public event Action SaveCompleted;

        public bool InProgress { get; private set; }

        public void Save(SaveMetaData saveToOverwrite = null)
        {
            if (InProgress)
            {
                _logger.LogError("Saving already in progress.");
                return;
            }

            InProgress = true;
            _logger.LogInformation("Saving game.");
            SaveStarted?.Invoke();

            var command = _commandFactory.Create(saveToOverwrite);
            SaveSafeToReturnToGameplay?.Invoke();

            _saving.Save(command, () =>
            {
                InProgress = false;
                SaveCompleted?.Invoke();
            });
        }
    }
}