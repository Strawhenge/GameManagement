namespace Strawhenge.GameManagement.Saving.Generator
{
    public interface ISaveDataGenerator<TSaveData>
    {
        TSaveData GenerateForCurrentGameState();
    }
}