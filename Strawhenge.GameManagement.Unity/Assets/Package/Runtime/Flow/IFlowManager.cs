namespace Strawhenge.GameManagement.Unity
{
    public interface IFlowManager
    {
        void StartNewGame();

        void LoadSave(SaveMetaData save);

        void MainMenu();

        void Quit();
    }
}