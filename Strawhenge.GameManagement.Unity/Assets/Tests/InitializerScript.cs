using Strawhenge.GameManagement.Unity.Tests.SaveData;
using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class InitializerScript : MonoBehaviour
    {
        static bool _isSaveRepositoryPopulated;

        void Awake()
        {
            PopulateSaveRepository();
        }

        void PopulateSaveRepository()
        {
            if (_isSaveRepositoryPopulated) return;
            _isSaveRepositoryPopulated = true;

            // TODO Change this.
            var saveDataRepository = GameManagement.SaveMetaDataRepository as InMemorySaveDataRepository;

            saveDataRepository!.Add(
                new SaveData.SaveData
                {
                    PlayerPosition = Vector3.zero,
                    SecondsToWait = 0
                },
                Guid.NewGuid(),
                DateTime.UtcNow.AddSeconds(-5));

            saveDataRepository.Add(
                new SaveData.SaveData
                {
                    PlayerPosition = new Vector3(0, 10, 0),
                    SecondsToWait = 3
                },
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(-1));

            saveDataRepository.Add(
                new SaveData.SaveData
                {
                    PlayerPosition = new Vector3(4, 0, 0),
                    SecondsToWait = 8
                },
                Guid.NewGuid(),
                DateTime.UtcNow.AddDays(-2));
        }
    }
}