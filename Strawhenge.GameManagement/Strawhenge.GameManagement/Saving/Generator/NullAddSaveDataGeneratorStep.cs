using System;

namespace Strawhenge.GameManagement.Saving
{
    public class NullAddSaveDataGeneratorStep<TSaveData> : IAddSaveDataGeneratorStep<TSaveData>
    {
        public static IAddSaveDataGeneratorStep<TSaveData> Instance { get; } =
            new NullAddSaveDataGeneratorStep<TSaveData>();

        NullAddSaveDataGeneratorStep()
        {
        }

        public void Add(Action<TSaveData> step)
        {
        }
    }
}