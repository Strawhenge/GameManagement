using Strawhenge.GameManagement.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.GameManagement.Unity
{
    public class GameManager : IGameManager
    {
        readonly ISaveDataSelector _saveDataSelector;
        readonly ISceneNames _sceneNames;
        readonly ILogger _logger;

        public GameManager(
            ISaveDataSelector saveDataSelector,
            ISceneNames sceneNames,
            ILogger logger)
        {
            _saveDataSelector = saveDataSelector;
            _sceneNames = sceneNames;
            _logger = logger;
        }

        public void StartNewGame()
        {
            _logger.LogInformation("Starting new game.");

            _saveDataSelector.SelectNewGame();
            SceneManager.LoadScene(_sceneNames.LoadingScreen);
        }

        public void LoadSave(SaveMetaData save)
        {
            _logger.LogInformation($"Loading save '{save.Id}'.");

            _saveDataSelector.SelectSave(save);
            SceneManager.LoadScene(_sceneNames.LoadingScreen);
        }

        public void MainMenu()
        {
            _logger.LogInformation("Returning to main menu.");
            SceneManager.LoadScene(_sceneNames.MainMenu);
        }

        public void Quit()
        {
            _logger.LogInformation("Quitting application.");
            Application.Quit();
        }
    }
}