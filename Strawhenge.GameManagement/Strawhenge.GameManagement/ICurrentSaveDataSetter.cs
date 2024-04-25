namespace Strawhenge.GameManagement.Unity
{
    public interface ICurrentSaveDataSetter<TSaveData>
    {
        void Set(TSaveData saveData);

        void SetDefault();
    }
}