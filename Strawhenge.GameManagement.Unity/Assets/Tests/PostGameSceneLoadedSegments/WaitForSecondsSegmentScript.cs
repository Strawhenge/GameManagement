using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Unity;
using System.Collections;
using UnityEngine;

public class WaitForSecondsSegmentScript : PostGameSceneLoadedSegmentScript
{
    bool _isCompleted;

    public ICurrentSaveDataAccessor<SaveData> SaveDataAccessor { private get; set; }

    public override bool IsCompleted => _isCompleted;

    public override void Run()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        var saveData = SaveDataAccessor.CurrentSaveData.Reduce(() => new SaveData());
        var seconds = saveData.SecondsToWait;

        yield return new WaitForSecondsRealtime(seconds);
        _isCompleted = true;
    }
}