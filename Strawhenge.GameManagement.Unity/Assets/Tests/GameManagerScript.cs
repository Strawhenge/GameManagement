using Strawhenge.GameManagement.Saving;

namespace Strawhenge.GameManagement.Unity.Tests.Tests
{
    public class GameManagerScript : BaseGameManagerScript<SaveData>
    {
        protected override ISaveRepository<SaveData> SaveRepository { get; } =
            new InMemorySaveDataRepository();

        protected override ISaveDataGenerator<SaveData> SaveDataGenerator { get; } =
            new SaveDataGenerator();
    }
}