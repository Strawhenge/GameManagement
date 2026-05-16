namespace Strawhenge.GameManagement.Saving.Generator
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