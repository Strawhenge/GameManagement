namespace Strawhenge.GameManagement
{
    public interface IDefaultSaveDataFactory<TSaveData>
    {
        TSaveData Create();
    }
}