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
        where TSaveData : class, new()
    {
        ILogger _logger;

        protected internal sealed override void RunSetup(SceneNames sceneNames)
        {
            var currentSaveDataContainer = new CurrentSaveDataContainer<TSaveData>();

            var selectedSaveDataController = new SelectedSaveDataController<TSaveData>(
                currentSaveDataContainer,
                SaveRepository);

            var gameManager = new FlowManager(
                selectedSaveDataController,
                sceneNames,
                Logger);

            var saveDataGeneratorSteps = new SaveDataGeneratorSteps<TSaveData>();
            var saveDataGenerator = new SaveDataGenerator<TSaveData>(saveDataGeneratorSteps);

            var saveCommandFactory = new SaveGameCommandFactory<TSaveData>(
                saveDataGenerator,
                SaveRepository);

            var pauseGame = new PauseGame(Logger);
            var restartGame = new RestartGame();

            var saving = this.GetOrAddComponent<SavingScript>();
            var saveGame = new SaveGame(saving, saveCommandFactory, Logger);

            GameManager.Flow = gameManager;
            GameManager.SceneNames = sceneNames;
            GameManager.SaveMetaDataRepository = SaveRepository;
            GameManager.PauseGame = pauseGame;
            GameManager.RestartGame = restartGame;
            GameManager.SaveGame = saveGame;
            GameManager.SelectedSaveDataController = selectedSaveDataController;
            GameManager.ClearSaveSaveGeneratorSteps = saveDataGeneratorSteps;
            GameManager<TSaveData>.CurrentSaveDataAccessor = currentSaveDataContainer;
            GameManager<TSaveData>.AddSaveDataGeneratorSteps = saveDataGeneratorSteps;
        }

        protected abstract ISaveRepository<TSaveData> SaveRepository { get; }

        protected virtual ILogger Logger => _logger ??= new UnityLogger(gameObject);
    }
}