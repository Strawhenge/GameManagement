namespace Strawhenge.GameManagement.Unity
{
    public interface IGameManager
    {
        void StartNewGame();

        void LoadSave(SaveMetaData save);

        void MainMenu();

        void Quit();
    }
}