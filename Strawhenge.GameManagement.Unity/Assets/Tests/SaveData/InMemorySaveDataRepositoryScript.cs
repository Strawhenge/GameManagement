using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.SaveData
{
    public class InMemorySaveDataRepositoryScript : MonoBehaviour
    {
        [SerializeField] SerializedSaveData[] _saves;

        internal InMemorySaveDataRepository Repository { get; } = new();

        void Awake()
        {
            foreach (var save in _saves)
            {
                var saveData = new SaveData
                {
                    PlayerPosition = save.PlayerPosition,
                    SecondsToWait = save.SecondsToWait
                };

                Repository.Add(
                    saveData,
                    Guid.NewGuid(),
                    DateTime.TryParse(save.DateTimeCreated, out var dateTimeCreated)
                        ? dateTimeCreated
                        : DateTime.UtcNow);
            }
        }
    }
}