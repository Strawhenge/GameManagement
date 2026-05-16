using System;
using System.Collections.Generic;

namespace Strawhenge.GameManagement.Saving
{
    public class SaveDataGeneratorSteps<TSaveData> : IClearSaveDataGeneratorSteps, IAddSaveDataGeneratorStep<TSaveData>
    {
        readonly List<Action<TSaveData>> _steps = new List<Action<TSaveData>>();

        public void Add(Action<TSaveData> step) => _steps.Add(step);

        public void Clear() => _steps.Clear();

        public void Run(TSaveData saveData) => _steps.ForEach(step => step(saveData));
    }
}