namespace Strawhenge.GameManagement.CurrentSaveData
{
    public interface ICurrentSaveDataAccessor<TSaveData>
    {
        TSaveData CurrentSaveData { get; }
    }
}