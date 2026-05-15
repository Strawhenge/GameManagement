using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public static class GameManagementSetup
    {
        public static void Run(string settingsResourcePath)
        {
            var settings = Resources.Load<GameManagementSettingsScriptableObject>(settingsResourcePath);

            if (settings == null)
            {
                Debug.LogError("Game Management settings not found.");
                return;
            }

            var gameManagerPrefab = settings.GameManagementPrefab;

            if (gameManagerPrefab == null)
            {
                Debug.LogError("Game Manager prefab not set.");
                return;
            }

            var mainMenuSceneName = settings.MainMenuSceneName;
            if (!Application.CanStreamedLevelBeLoaded(mainMenuSceneName))
            {
                Debug.LogError($"Scene '{mainMenuSceneName}' cannot be loaded.");
                return;
            }

            var loadingScreenSceneName = settings.LoadingScreenSceneName;
            if (!Application.CanStreamedLevelBeLoaded(loadingScreenSceneName))
            {
                Debug.LogError($"Scene '{loadingScreenSceneName}' cannot be loaded.");
                return;
            }

            var gameSceneName = settings.GameSceneName;
            if (!Application.CanStreamedLevelBeLoaded(gameSceneName))
            {
                Debug.LogError($"Scene '{gameSceneName}' cannot be loaded.");
                return;
            }

            var sceneNames = new SceneNames(mainMenuSceneName, loadingScreenSceneName, gameSceneName);

            var gameManagerScript = Object.Instantiate(gameManagerPrefab);
            Object.DontDestroyOnLoad(gameManagerScript.gameObject);
            gameManagerScript.RunSetup(sceneNames);
        }
    }
}