using System.Threading.Tasks;

namespace Strawhenge.GameManagement.Loading
{
    public interface ISelectedSaveDataLoader
    {
        Task LoadProgress();
    }
}