using FunctionalUtilities;
using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public abstract class PostGameSceneLoadedSegmentScript : MonoBehaviour
    {
        public abstract bool IsCompleted { get; }

        public abstract void Run();
    }

    public abstract class PostGameSceneLoadedSegmentScript<TSaveData> : PostGameSceneLoadedSegmentScript
        where TSaveData : class, new()
    {
        protected Maybe<TSaveData> CurrentSaveData =>
            GameManager<TSaveData>.CurrentSaveDataAccessor.CurrentSaveData;

        protected void AddSaveDataGeneratorStep(Action<TSaveData> step) =>
            GameManager<TSaveData>.SaveDataGeneratorSteps.Add(step);
    }
}