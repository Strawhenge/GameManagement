using Strawhenge.Common.Unity;
using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;
using Strawhenge.GameManagement.Unity;
using System;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

public class InitializerScript : MonoBehaviour
{
    static ILogger _logger;
    static CurrentSaveDataContainer<SaveData> _currentSaveDataContainer;
    static InMemorySaveDataRepository _saveDataRepository;
    static SelectedSaveDataController<SaveData> _saveDataSelectorController;
    static SceneNames _sceneNames;
    static GameManager _gameManager;
    static PauseGame _pauseGame;
    static SaveGameCommandFactory<SaveData> _saveGameCommandFactory;
    static SaveDataGenerator _saveDataGenerator;
    static RestartGame _restartGame;
    static bool _isSaveRepositoryPopulated;

    void Awake()
    {
        _logger ??= GlobalUnityLogger.Instance;


        _currentSaveDataContainer ??= new CurrentSaveDataContainer<SaveData>();

        _saveDataRepository ??= new InMemorySaveDataRepository();

        _saveDataSelectorController ??= new SelectedSaveDataController<SaveData>(
            _currentSaveDataContainer,
            _saveDataRepository);

        _sceneNames ??= new SceneNames();

        _gameManager ??= new GameManager(
            _saveDataSelectorController,
            _sceneNames,
            _logger);

        _pauseGame = new PauseGame(_logger);

        _saveDataGenerator ??= new SaveDataGenerator();

        _saveGameCommandFactory ??= new SaveGameCommandFactory<SaveData>(
            _saveDataGenerator,
            _saveDataRepository);

        _restartGame ??= new RestartGame();

        if (FindObjectOfType<MainMenuScript>() is MainMenuScript mainMenu)
        {
            mainMenu.GameManager = _gameManager;
            mainMenu.SaveMetaDataRepository = _saveDataRepository;
        }

        if (FindObjectOfType<SaveDataMenuScript>() is SaveDataMenuScript saveDataMenu)
        {
            saveDataMenu.SaveMetaDataRepository = _saveDataRepository;
        }

        if (FindObjectOfType<LoadingScreenScript>() is LoadingScreenScript loadingScreen)
        {
            loadingScreen.SelectedSaveDataLoader = _saveDataSelectorController;
            loadingScreen.SceneNames = _sceneNames;
        }

        if (FindObjectOfType<PauseMenuScript>() is PauseMenuScript pauseMenu)
        {
            pauseMenu.GameManager = _gameManager;
            pauseMenu.PauseGame = _pauseGame;
        }

        if (FindObjectOfType<SavingScript>() is SavingScript saving)
        {
            saving.SaveGameCommandFactory = _saveGameCommandFactory;
        }

        if (FindObjectOfType<RestartMenuScript>() is RestartMenuScript restartMenu)
        {
            restartMenu.GameManager = _gameManager;
            restartMenu.SaveMetaDataRepository = _saveDataRepository;
            restartMenu.RestartGame = _restartGame;
        }

        if (FindObjectOfType<PostGameSceneLoadedScript>() is PostGameSceneLoadedScript postGameSceneLoaded)
        {
            postGameSceneLoaded.SaveDataState = _saveDataSelectorController;
            postGameSceneLoaded.SceneNames = _sceneNames;
        }

        if (FindObjectOfType<PlayerPositionSegmentScript>() is PlayerPositionSegmentScript playerPositionSegment)
        {
            playerPositionSegment.SaveDataAccessor = _currentSaveDataContainer;
            playerPositionSegment.SaveDataGenerator = _saveDataGenerator;
        }

        if (FindObjectOfType<WaitForSecondsSegmentScript>() is WaitForSecondsSegmentScript waitForSecondsSegment)
        {
            waitForSecondsSegment.SaveDataAccessor = _currentSaveDataContainer;
        }

        if (FindObjectOfType<InputScript>() is InputScript input)
        {
            input.PauseGame = _pauseGame;
            input.RestartGame = _restartGame;
        }

        PopulateSaveRepository();
    }

    void PopulateSaveRepository()
    {
        if (_isSaveRepositoryPopulated) return;
        _isSaveRepositoryPopulated = true;

        _saveDataRepository.Add(
            new SaveData
            {
                PlayerPosition = Vector3.zero,
                SecondsToWait = 0
            },
            Guid.NewGuid(),
            DateTime.UtcNow.AddSeconds(-5));

        _saveDataRepository.Add(
            new SaveData
            {
                PlayerPosition = new Vector3(0, 10, 0),
                SecondsToWait = 3
            },
            Guid.NewGuid(),
            DateTime.UtcNow.AddHours(-1));

        _saveDataRepository.Add(
            new SaveData
            {
                PlayerPosition = new Vector3(4, 0, 0),
                SecondsToWait = 8
            },
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(-2));
    }
}