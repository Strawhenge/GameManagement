namespace Strawhenge.GameManagement.Unity.Setup
{
    public class SceneNames
    {
        public static SceneNames Empty { get; } = new SceneNames(
            string.Empty,
            string.Empty,
            string.Empty);

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