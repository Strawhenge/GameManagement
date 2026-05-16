using Strawhenge.GameManagement.SaveRepository;

namespace Strawhenge.GameManagement.Unity.Flow
{
    class NullFlowManager : IFlowManager
    {
        public static IFlowManager Instance { get; } = new NullFlowManager();

        NullFlowManager()
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