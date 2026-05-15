namespace Strawhenge.GameManagement.Unity
{
    class NullGameManager : IGameManager
    {
        public static IGameManager Instance { get; } = new NullGameManager();

        NullGameManager()
        {
        }

        public void StartNewGame()
        {
        }

        public void LoadSave(SaveMetaData save)
        {
        }

        public void MainMenu()
        {
        }

        public void Quit()
        {
        }
    }
}