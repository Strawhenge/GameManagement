using Strawhenge.Common.Unity;
using Strawhenge.GameManagement.SaveRepository;
using Strawhenge.GameManagement.Unity.Setup;
using Strawhenge.GameManagement.Unity.Tests.SaveData;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class GameManagementScript : BaseGameManagementScript<SaveData.SaveData>
    {
        [SerializeField] InMemorySaveDataRepositoryScript _repository;
        [SerializeField] LoggerScript _logger;

        InMemorySaveDataRepository _saveRepository;

        protected override ISaveRepository<SaveData.SaveData> SaveRepository => _saveRepository ??= CreateRepository();

        protected override ILogger Logger => _logger != null
            ? _logger.Logger
            : base.Logger;

        InMemorySaveDataRepository CreateRepository()
        {
            return _repository != null
                ? _repository.Repository
                : new InMemorySaveDataRepository();
        }
    }
}