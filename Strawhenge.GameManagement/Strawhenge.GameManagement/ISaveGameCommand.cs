using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Unity
{
    public interface ISaveGameCommand
    {
        Task SaveAsync();
    }
}