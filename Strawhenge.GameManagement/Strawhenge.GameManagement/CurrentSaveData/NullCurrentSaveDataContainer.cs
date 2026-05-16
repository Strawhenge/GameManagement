using FunctionalUtilities;

namespace Strawhenge.GameManagement.CurrentSaveData
{
    public class NullCurrentSaveDataContainer<TSaveData> : ICurrentSaveDataSetter<TSaveData>, ICurrentSaveDataAccessor<TSaveData>
    {
        public static NullCurrentSaveDataContainer<TSaveData> Instance { get; } = new NullCurrentSaveDataContainer<TSaveData>();

        NullCurrentSaveDataContainer()
        {
        }

        public Maybe<TSaveData> CurrentSaveData => Maybe.None<TSaveData>();

        public void Set(TSaveData saveData)
        {
        }

        public void Reset()
        {
        }
    }
}
