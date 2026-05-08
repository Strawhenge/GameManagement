namespace Strawhenge.GameManagement.Unity
{
    public class SceneNames
    {
        public SceneNames(string mainMenu, string loadingScreen, string game)
        {
            MainMenu = mainMenu;
            LoadingScreen = loadingScreen;
            Game = game;
        }

        public string MainMenu { get; }

        public string LoadingScreen { get; }

        public string Game { get; }
    }
}