using FunctionalUtilities;

namespace Strawhenge.GameManagement.CurrentSaveData
{
    public class CurrentSaveDataContainer<TSaveData> :
        ICurrentSaveDataSetter<TSaveData>,
        ICurrentSaveDataAccessor<TSaveData>
    {
        public Maybe<TSaveData> CurrentSaveData { get; private set; } = Maybe.None<TSaveData>();

        public void Set(TSaveData saveData)
        {
            CurrentSaveData = saveData;
        }

        public void Reset()
        {
            CurrentSaveData = Maybe.None<TSaveData>();
        }
    }
}