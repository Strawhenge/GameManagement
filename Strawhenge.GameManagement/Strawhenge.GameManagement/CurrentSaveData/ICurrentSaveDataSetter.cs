namespace Strawhenge.GameManagement.CurrentSaveData
{
    public interface ICurrentSaveDataSetter<TSaveData>
    {
        void Set(TSaveData saveData);

        void SetDefault();
    }
}