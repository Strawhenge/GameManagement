using Strawhenge.Common.Unity;
using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.GameManagement.Unity
{
    public abstract class BaseGameManagementScript : MonoBehaviour
    {
        protected internal abstract void RunSetup(SceneNames sceneNames);
    }

    public abstract class BaseGameManagementScript<TSaveData> : BaseGameManagementScript
    {
        ILogger _logger;

        protected internal sealed override void RunSetup(SceneNames sceneNames)
        {
            var currentSaveDataContainer = new CurrentSaveDataContainer<TSaveData>();

            var selectedSaveDataController = new SelectedSaveDataController<TSaveData>(
                currentSaveDataContainer,
                SaveRepository);

            var gameManager = new GameManager(
                selectedSaveDataController,
                sceneNames,
                Logger);

            var saveCommandFactory = new SaveGameCommandFactory<TSaveData>(
                SaveDataGenerator,
                SaveRepository);

            var pauseGame = new PauseGame(Logger);
            var restartGame = new RestartGame();

            var saving = this.GetOrAddComponent<SavingScript>();
            var saveGame = new SaveGame(saving, saveCommandFactory, Logger);

            GameManagement.GameManager = gameManager;
            GameManagement.SceneNames = sceneNames;
            GameManagement.SaveMetaDataRepository = SaveRepository;
            GameManagement.PauseGame = pauseGame;
            GameManagement.RestartGame = restartGame;
            GameManagement.SaveGame = saveGame;
            GameManagement.SelectedSaveDataLoader = selectedSaveDataController;
            GameManagement.SelectedSaveDataState = selectedSaveDataController;
            GameManagement<TSaveData>.CurrentSaveDataAccessor = currentSaveDataContainer;
        }

        protected abstract ISaveRepository<TSaveData> SaveRepository { get; }

        protected abstract ISaveDataGenerator<TSaveData> SaveDataGenerator { get; }

        protected virtual ILogger Logger => _logger ??= new UnityLogger(gameObject);
    }
}