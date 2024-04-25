using Strawhenge.GameManagement.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.GameManagement.Unity
{
    public class GameManager : IGameManager
    {
        readonly ISaveDataSelector _saveDataSelector;
        readonly ILogger _logger;

        public GameManager(ISaveDataSelector saveDataSelector, ILogger logger)
        {
            _saveDataSelector = saveDataSelector;
            _logger = logger;
        }

        public void StartNewGame()
        {
            _logger.LogInformation("Starting new game.");

            _saveDataSelector.SelectNewGame();
            SceneManager.LoadScene("LoadingScreen");
        }

        public void LoadSave(SaveMetaData save)
        {
            _logger.LogInformation($"Loading save '{save.Id}'.");

            _saveDataSelector.SelectSave(save);
            SceneManager.LoadScene("LoadingScreen");
        }

        public void MainMenu()
        {
            _logger.LogInformation("Returning to main menu.");
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            _logger.LogInformation("Quitting application.");
            Application.Quit();
        }
    }
}