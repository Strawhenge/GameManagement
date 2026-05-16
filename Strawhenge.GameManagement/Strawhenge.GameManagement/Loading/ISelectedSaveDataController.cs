using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Loading
{
    public interface ISelectedSaveDataController
    {
        bool IsAwaitingSelectedSaveDataLoad { get; }

        void SelectNewGame();

        void SelectSave(SaveMetaData saveMetaData);

        Task LoadProgress();
    }
}