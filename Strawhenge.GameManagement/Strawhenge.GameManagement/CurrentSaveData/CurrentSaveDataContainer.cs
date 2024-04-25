namespace Strawhenge.GameManagement.CurrentSaveData
{
    public class CurrentSaveDataContainer<TSaveData>
        : ICurrentSaveDataSetter<TSaveData>, ICurrentSaveDataAccessor<TSaveData>
    {
        readonly TSaveData _defaultSaveData;

        public CurrentSaveDataContainer(IDefaultSaveDataFactory<TSaveData> defaultSaveDataFactory)
        {
            _defaultSaveData = defaultSaveDataFactory.Create();
            CurrentSaveData = _defaultSaveData;
        }

        public TSaveData CurrentSaveData { get; private set; }

        public void Set(TSaveData saveData)
        {
            CurrentSaveData = saveData;
        }

        public void SetDefault()
        {
            CurrentSaveData = _defaultSaveData;
        }
    }
}