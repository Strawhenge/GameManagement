using System.Collections;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments
{
    public class WaitForSecondsSegmentScript : PostGameSceneLoadedSegmentScript<SaveData.SaveData>
    {
        bool _isCompleted;

        public override bool IsCompleted => _isCompleted;

        public override void Run()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            var saveData = CurrentSaveData.Reduce(() => new SaveData.SaveData());
            var seconds = saveData.SecondsToWait;

            yield return new WaitForSecondsRealtime(seconds);
            _isCompleted = true;
        }
    }
}