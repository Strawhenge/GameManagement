namespace Strawhenge.GameManagement.Unity
{
    public interface ISaveDataGenerator<TSaveData>
    {
        TSaveData GenerateForCurrentGameState();
    }
}