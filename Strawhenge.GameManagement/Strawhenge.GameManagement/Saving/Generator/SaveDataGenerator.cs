namespace Strawhenge.GameManagement.Saving
{
    public class SaveDataGenerator<TSaveData> : ISaveDataGenerator<TSaveData> where TSaveData : class, new()
    {
        readonly SaveDataGeneratorSteps<TSaveData> _steps;

        public SaveDataGenerator(SaveDataGeneratorSteps<TSaveData> steps)
        {
            _steps = steps;
        }

        public TSaveData GenerateForCurrentGameState()
        {
            var saveData = new TSaveData();
            _steps.Run(saveData);
            return saveData;
        }
    }
}