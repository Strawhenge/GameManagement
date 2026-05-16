using Strawhenge.GameManagement.SaveRepository;

namespace Strawhenge.GameManagement.Saving
{
    public interface ISaveGameCommandFactory
    {
        ISaveGameCommand Create(SaveMetaData saveToOverwrite = null);
    }
}