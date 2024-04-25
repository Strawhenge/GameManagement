namespace Strawhenge.GameManagement.Loading
{
    public interface ISaveDataSelector
    {
        void SelectNewGame();

        void SelectSave(SaveMetaData saveMetaData);
    }
}