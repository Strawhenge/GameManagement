using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Loading
{
    public class NullSelectedSaveDataController : ISaveDataSelector, ISelectedSaveDataLoader, ISelectedSaveDataState
    {
        public static NullSelectedSaveDataController Instance { get; } = new NullSelectedSaveDataController();

        NullSelectedSaveDataController()
        {
        }

        public bool IsAwaitingSelectedSaveDataLoad => false;

        public void SelectNewGame()
        {
        }

        public void SelectSave(SaveMetaData saveMetaData)
        {
        }

        public Task LoadProgress() => Task.CompletedTask;
    }
}
