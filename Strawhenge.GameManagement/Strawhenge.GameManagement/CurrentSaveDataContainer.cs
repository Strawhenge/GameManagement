namespace Strawhenge.GameManagement.Unity
{
    public class CurrentSaveDataContainer<TSaveData>
        : ICurrentSaveDataSetter<TSaveData>, ICurrentSaveDataAccessor<TSaveData>
    {
        readonly TSaveData _newGameData;

        public CurrentSaveDataContainer(IDefaultSaveDataFactory<TSaveData> defaultSaveDataFactory)
        {
            _newGameData = defaultSaveDataFactory.Create();
            CurrentSaveData = _newGameData;
        }

        public TSaveData CurrentSaveData { get; private set; }

        public void Set(TSaveData saveData)
        {
            CurrentSaveData = saveData;
        }

        public void SetNewGame()
        {
            CurrentSaveData = _newGameData;
        }
    }
}