using Strawhenge.Common.Unity;
using Strawhenge.GameManagement.Saving;
using Strawhenge.GameManagement.Unity.Tests.SaveData;
using System;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class GameManagementScript : BaseGameManagementScript<SaveData.SaveData>
    {
        [SerializeField] LoggerScript _logger;

        readonly InMemorySaveDataRepository _saveRepository = new();

        protected override ISaveRepository<SaveData.SaveData> SaveRepository => _saveRepository;

        protected override ILogger Logger => _logger != null
            ? _logger.Logger
            : base.Logger;

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