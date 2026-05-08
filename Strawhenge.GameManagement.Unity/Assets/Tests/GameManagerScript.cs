using Strawhenge.GameManagement.Saving;
using Strawhenge.GameManagement.Unity.Tests.SaveData;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class GameManagerScript : BaseGameManagerScript<SaveData.SaveData>
    {
        protected override ISaveRepository<SaveData.SaveData> SaveRepository { get; } =
            new InMemorySaveDataRepository();

        protected override ISaveDataGenerator<SaveData.SaveData> SaveDataGenerator { get; } =
            new SaveDataGenerator();
    }
}