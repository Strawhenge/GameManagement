namespace Strawhenge.GameManagement.Unity
{
    public interface ICurrentSaveDataAccessor<TSaveData>
    {
        TSaveData CurrentSaveData { get; }
    }
}