using Strawhenge.GameManagement.Saving;

class SaveDataGenerator : ISaveDataGenerator<SaveData>
{
    public SaveData GenerateForCurrentGameState()
    {
        return new SaveData();
    }
}