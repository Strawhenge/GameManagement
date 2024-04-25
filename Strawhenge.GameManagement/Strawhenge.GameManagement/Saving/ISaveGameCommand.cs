using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Saving
{
    public interface ISaveGameCommand
    {
        Task SaveAsync();
    }
}