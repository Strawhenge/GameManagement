namespace Strawhenge.GameManagement.Unity
{
    public interface ISaveDataSelector
    {
        void SelectNewGame();

        void SelectSave(SaveMetaData saveMetaData);
    }
}