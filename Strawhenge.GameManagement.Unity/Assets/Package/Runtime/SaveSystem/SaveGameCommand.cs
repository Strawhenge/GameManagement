using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveGameCommand<TSaveData> : ISaveGameCommand
    {
        readonly ISaveDataRepository<TSaveData> _repository;
        readonly TSaveData _data;
        readonly SaveMetaData _saveToOverwrite;

        public SaveGameCommand(
            ISaveDataRepository<TSaveData> repository,
            TSaveData data,
            SaveMetaData saveToOverwrite = null)
        {
            _repository = repository;
            _data = data;
            _saveToOverwrite = saveToOverwrite;
        }

        public async Task SaveAsync()
        {
            var task = _repository.SaveAsync(_data);

            if (_saveToOverwrite != null)
                await _repository.DeleteAsync(_saveToOverwrite.Id);

            await task;
        }
    }
}