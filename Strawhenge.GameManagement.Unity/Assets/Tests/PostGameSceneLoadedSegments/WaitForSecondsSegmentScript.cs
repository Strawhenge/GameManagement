using System.Collections;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments
{
    public class WaitForSecondsSegmentScript : PostGameSceneLoadedSegmentScript
    {
        bool _isCompleted;

        public override bool IsCompleted => _isCompleted;

        public override void Run()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            var saveData = GameManagement<SaveData.SaveData>.CurrentSaveDataAccessor.CurrentSaveData.Reduce(() => new SaveData.SaveData());
            var seconds = saveData.SecondsToWait;

            yield return new WaitForSecondsRealtime(seconds);
            _isCompleted = true;
        }
    }
}