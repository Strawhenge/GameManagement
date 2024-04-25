using System;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.GameManagement.Unity
{
    public class PauseGame : IPauseGame
    {
        readonly ILogger _logger;

        public PauseGame(ILogger logger)
        {
            _logger = logger;
        }

        public event Action Paused;
        public event Action Resumed;

        public bool IsPaused { get; private set; }

        public void Pause()
        {
            if (IsPaused) return;

            _logger.LogInformation("Pausing game.");

            AudioListener.pause = true;
            Time.timeScale = 0;

            IsPaused = true;
            Paused?.Invoke();
        }

        public void Resume()
        {
            if (!IsPaused) return;

            _logger.LogInformation("Resuming game.");

            AudioListener.pause = false;
            Time.timeScale = 1;

            IsPaused = false;
            Resumed?.Invoke();
        }
    }
}