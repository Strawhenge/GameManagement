namespace Strawhenge.GameManagement.Saving
{
    public interface ISaveDataGenerator<TSaveData>
    {
        TSaveData GenerateForCurrentGameState();
    }
}