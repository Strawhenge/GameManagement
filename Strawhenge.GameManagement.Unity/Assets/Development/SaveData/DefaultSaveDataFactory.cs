using Strawhenge.GameManagement;

class DefaultSaveDataFactory : IDefaultSaveDataFactory<SaveData>
{
    public SaveData Create()
    {
        return new SaveData();
    }
}