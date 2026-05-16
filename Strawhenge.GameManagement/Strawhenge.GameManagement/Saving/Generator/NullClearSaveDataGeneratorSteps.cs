namespace Strawhenge.GameManagement.Saving
{
    public class NullClearSaveDataGeneratorSteps : IClearSaveDataGeneratorSteps
    {
        public static IClearSaveDataGeneratorSteps Instance { get; } = new NullClearSaveDataGeneratorSteps();

        NullClearSaveDataGeneratorSteps()
        {
        }

        public void Clear()
        {
        }
    }
}