using System.Threading.Tasks;
using Strawhenge.GameManagement.CurrentSaveData;

namespace Strawhenge.GameManagement.Loading
{
    public class SelectedSaveDataController<TSaveData> : ISaveDataSelector, ISelectedSaveDataLoader, ISelectedSaveDataState
    {
        readonly ICurrentSaveDataSetter<TSaveData> _currentSaveData;
        readonly ISaveDataRepository<TSaveData> _repository;

        SaveMetaData _selectedSave;

        public SelectedSaveDataController(
            ICurrentSaveDataSetter<TSaveData> currentSaveData,
            ISaveDataRepository<TSaveData> repository)
        {
            _currentSaveData = currentSaveData;
            _repository = repository;
        }

        public bool IsAwaitingSelectedSaveDataLoad { get; private set; }

        public void SelectNewGame()
        {
            _selectedSave = null;
            IsAwaitingSelectedSaveDataLoad = true;
        }

        public void SelectSave(SaveMetaData saveMetaData)
        {
            _selectedSave = saveMetaData;
            IsAwaitingSelectedSaveDataLoad = true;
        }

        public async Task LoadProgress()
        {
            if (_selectedSave == null)
            {
                _currentSaveData.SetDefault();
            }
            else
            {
                var save = await _repository.GetAsync(_selectedSave.Id);
                _currentSaveData.Set(save);
            }

            IsAwaitingSelectedSaveDataLoad = false;
        }
    }
}