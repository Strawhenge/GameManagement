namespace Strawhenge.GameManagement.Saving
{
    public class SaveGameCommandFactory<TSaveData> : ISaveGameCommandFactory
    {
        readonly ISaveDataGenerator<TSaveData> _saveDataGenerator;
        readonly ISaveDataRepository<TSaveData> _repository;

        public SaveGameCommandFactory(
            ISaveDataGenerator<TSaveData> saveDataGenerator,
            ISaveDataRepository<TSaveData> repository)
        {
            _saveDataGenerator = saveDataGenerator;
            _repository = repository;
        }

        public ISaveGameCommand Create(SaveMetaData saveToOverwrite = null)
        {
            var data = _saveDataGenerator.GenerateForCurrentGameState();

            return new SaveGameCommand<TSaveData>(_repository, data, saveToOverwrite);
        }
    }
}