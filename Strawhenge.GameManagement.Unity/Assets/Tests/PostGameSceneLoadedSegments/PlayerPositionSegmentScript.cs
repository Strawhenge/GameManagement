using Strawhenge.GameManagement.Unity.Tests.SaveData;
using System.Collections;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments
{
    public class PlayerPositionSegmentScript : PostGameSceneLoadedSegmentScript
    {
        [SerializeField] Rigidbody _player;

        bool _isCompleted;

        public override bool IsCompleted => _isCompleted;

        public override void Run()
        {
            SaveDataGenerator.Player = _player;
            StartCoroutine(SetPlayerPosition());
        }

        IEnumerator SetPlayerPosition()
        {
            var saveData = GameManagement<SaveData.SaveData>.CurrentSaveDataAccessor.CurrentSaveData.Reduce(() => new SaveData.SaveData());
            var position = saveData.PlayerPosition;

            _player.position = position;
            _isCompleted = true;
            yield return null;
        }
    }
}