using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    [CreateAssetMenu(menuName = "Strawhenge/Game Management/Settings")]
    public class GameManagementSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] SerializedSceneName _mainMenuScene;
        [SerializeField] SerializedSceneName _loadingScreenScene;
        [SerializeField] SerializedSceneName _gameScene;
        [SerializeField] BaseGameManagementScript _gameManagementPrefab;

        public BaseGameManagementScript GameManagementPrefab => _gameManagementPrefab;

        public string MainMenuSceneName => _mainMenuScene.Name;

        public string LoadingScreenSceneName => _loadingScreenScene.Name;

        public string GameSceneName => _gameScene.Name;
    }
}