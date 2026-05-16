namespace Strawhenge.GameManagement.SaveRepository
{
    public interface ISaveRepository<TSaveData> : ISaveDataRepository<TSaveData>, ISaveMetaDataRepository
    {
    }
}