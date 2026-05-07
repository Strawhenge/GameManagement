using Strawhenge.Common.Unity;
using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public abstract class BaseGameManagerScript : MonoBehaviour
    {
        protected internal abstract void RunSetup(ISceneNames sceneNames);
    }

    public abstract class BaseGameManagerScript<TSaveData> : BaseGameManagerScript
    {
        protected internal sealed override void RunSetup(ISceneNames sceneNames)
        {
            var logger = new UnityLogger(gameObject); // TODO Add LoggerScript field.

            var currentSaveDataContainer = new CurrentSaveDataContainer<TSaveData>();

            var selectedSaveDataController = new SelectedSaveDataController<TSaveData>(
                currentSaveDataContainer,
                SaveRepository);

            var gameManager = new GameManager(
                selectedSaveDataController,
                sceneNames,
                logger);

            var saveCommandFactory = new SaveGameCommandFactory<TSaveData>(
                null,
                SaveRepository);

            var pauseGame = new PauseGame(logger);
            var restartGame = new RestartGame();

            GameManagement.GameManager = gameManager;
            GameManagement.SceneNames = sceneNames;
            GameManagement.SaveMetaDataRepository = SaveRepository;
            GameManagement.PauseGame = pauseGame;
            GameManagement.RestartGame = restartGame;
            GameManagement.SaveCommandFactory = saveCommandFactory;
            GameManagement.SelectedSaveDataLoader = selectedSaveDataController;
            GameManagement.SelectedSaveDataState = selectedSaveDataController;
            GameManagement<TSaveData>.CurrentSaveDataAccessor = currentSaveDataContainer;
        }

        protected abstract ISaveRepository<TSaveData> SaveRepository { get; }

        protected abstract ISaveDataGenerator<TSaveData> SaveDataGenerator { get; }
    }
}