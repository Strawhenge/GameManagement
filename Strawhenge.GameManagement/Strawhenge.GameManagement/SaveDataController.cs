using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveDataController<TSaveData> : ISaveDataSelector, ISelectedSaveDataLoader, ISaveDataState
    {
        readonly ICurrentSaveDataSetter<TSaveData> _currentSaveData;
        readonly ISaveDataRepository<TSaveData> _repository;

        SaveMetaData _selectedSave;

        public SaveDataController(
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
                _currentSaveData.SetNewGame();
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