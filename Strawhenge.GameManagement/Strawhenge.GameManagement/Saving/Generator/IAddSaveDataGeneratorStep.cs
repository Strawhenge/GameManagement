using System;

namespace Strawhenge.GameManagement.Saving
{
    public interface IAddSaveDataGeneratorStep<TSaveData>
    {
        void Add(Action<TSaveData> step);
    }
}