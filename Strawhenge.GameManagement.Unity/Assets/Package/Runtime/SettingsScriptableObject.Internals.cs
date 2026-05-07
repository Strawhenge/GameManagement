namespace Strawhenge.GameManagement.Unity
{
    public partial class GameManagementSettingsScriptableObject
    {
        internal static string GameManagerPrefabFieldName => nameof(_gameManagerPrefab);

        internal static string LoadingScreenSceneFieldName => nameof(_loadingScreenScene);

        internal static string MainMenuSceneFieldName => nameof(_mainMenuScene);

        internal static string GameSceneFieldName => nameof(_gameScene);
    }
}