using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Unity;
using System.Collections;
using UnityEngine;

public class PlayerPositionSegmentScript : PostGameSceneLoadedSegmentScript
{
    [SerializeField] Rigidbody _player;

    public ICurrentSaveDataAccessor<SaveData> SaveDataAccessor { private get; set; }

    bool _isCompleted;

    public override bool IsCompleted => _isCompleted;

    public override void Run()
    {
        StartCoroutine(SetPlayerPosition());
    }

    IEnumerator SetPlayerPosition()
    {
        var saveData = SaveDataAccessor.CurrentSaveData;
        var position = saveData.PlayerPosition;

        _player.position = position;
        _isCompleted = true;
        yield return null;
    }
}