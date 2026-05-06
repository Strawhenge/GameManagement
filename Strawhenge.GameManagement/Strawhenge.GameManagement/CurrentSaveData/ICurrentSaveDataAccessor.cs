using FunctionalUtilities;

namespace Strawhenge.GameManagement.CurrentSaveData
{
    public interface ICurrentSaveDataAccessor<TSaveData>
    {
        Maybe<TSaveData> CurrentSaveData { get; }
    }
}