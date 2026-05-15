namespace Strawhenge.GameManagement
{
    public interface ISaveRepository<TSaveData> : ISaveDataRepository<TSaveData>, ISaveMetaDataRepository
    {
    }
}