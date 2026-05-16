using Strawhenge.GameManagement.Unity.GameLoading;
using System.Collections;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments
{
    public class PlayerPositionSegmentScript : PostGameSceneLoadedSegmentScript<SaveData.SaveData>
    {
        [SerializeField] Rigidbody _player;

        bool _isCompleted;

        public override bool IsCompleted => _isCompleted;

        public override void Run()
        {
            AddSaveDataGeneratorStep(saveData =>
                saveData.PlayerPosition = _player.position);

            StartCoroutine(SetPlayerPosition());
        }

        IEnumerator SetPlayerPosition()
        {
            var saveData = CurrentSaveData.Reduce(() => new SaveData.SaveData());
            var position = saveData.PlayerPosition;

            _player.position = position;
            _isCompleted = true;
            yield return null;
        }
    }
}