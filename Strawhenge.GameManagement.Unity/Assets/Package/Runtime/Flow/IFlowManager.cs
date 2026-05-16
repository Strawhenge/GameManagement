using Strawhenge.GameManagement.SaveRepository;

namespace Strawhenge.GameManagement.Unity.Flow
{
    public interface IFlowManager
    {
        void StartNewGame();

        void LoadSave(SaveMetaData save);

        void MainMenu();

        void Quit();
    }
}