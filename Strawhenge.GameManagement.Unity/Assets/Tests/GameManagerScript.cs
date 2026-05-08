using Strawhenge.GameManagement.Saving;
using Strawhenge.GameManagement.Unity.Tests.SaveData;
using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class GameManagerScript : BaseGameManagerScript<SaveData.SaveData>
    {
        readonly InMemorySaveDataRepository _saveRepository = new();

        protected override ISaveRepository<SaveData.SaveData> SaveRepository => _saveRepository;

        protected override ISaveDataGenerator<SaveData.SaveData> SaveDataGenerator { get; } =
            new SaveDataGenerator();

        void Awake()
        {
            _saveRepository!.Add(
                new SaveData.SaveData
                {
                    PlayerPosition = Vector3.zero,
                    SecondsToWait = 0
                },
                Guid.NewGuid(),
                DateTime.UtcNow.AddSeconds(-5));

            _saveRepository.Add(
                new SaveData.SaveData
                {
                    PlayerPosition = new Vector3(0, 10, 0),
                    SecondsToWait = 3
                },
                Guid.NewGuid(),
                DateTime.UtcNow.AddHours(-1));

            _saveRepository.Add(
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