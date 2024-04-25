namespace Strawhenge.GameManagement.Unity
{
    public interface IDefaultSaveDataFactory<TSaveData>
    {
        TSaveData Create();
    }
}