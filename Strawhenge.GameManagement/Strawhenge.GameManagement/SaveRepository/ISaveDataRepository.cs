using System;
using System.Threading.Tasks;

namespace Strawhenge.GameManagement.SaveRepository
{
    public interface ISaveDataRepository<TSaveData>
    {
        Task<TSaveData> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task SaveAsync(TSaveData saveData);
    }
}