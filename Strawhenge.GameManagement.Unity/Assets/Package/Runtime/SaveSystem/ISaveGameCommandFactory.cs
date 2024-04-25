namespace Strawhenge.GameManagement.Unity
{
    public interface ISaveGameCommandFactory
    {
        ISaveGameCommand Create(SaveMetaData saveToOverwrite = null);
    }
}