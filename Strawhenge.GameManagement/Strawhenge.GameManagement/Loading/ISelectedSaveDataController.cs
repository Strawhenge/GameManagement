using System.Threading.Tasks;
using Strawhenge.GameManagement.SaveRepository;

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