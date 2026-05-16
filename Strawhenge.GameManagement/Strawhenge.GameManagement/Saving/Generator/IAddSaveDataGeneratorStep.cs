using System;

namespace Strawhenge.GameManagement.Saving.Generator
{
    public interface IAddSaveDataGeneratorStep<TSaveData>
    {
        void Add(Action<TSaveData> step);
    }
}