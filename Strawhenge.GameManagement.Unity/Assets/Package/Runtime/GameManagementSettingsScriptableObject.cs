using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public partial class GameManagementSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] SerializedSceneName _mainMenuScene;
        [SerializeField] SerializedSceneName _loadingScreenScene;
        [SerializeField] SerializedSceneName _gameScene;
        [SerializeField] BaseGameManagerScript _gameManagerPrefab;

        public BaseGameManagerScript GameManagerPrefab => _gameManagerPrefab;

        public string MainMenuSceneName => _mainMenuScene.Name;

        public string LoadingScreenSceneName => _loadingScreenScene.Name;

        public string GameSceneName => _gameScene.Name;
    }
}