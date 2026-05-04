using FunctionalUtilities;
using Strawhenge.Common.Unity;
using Strawhenge.GameManagement;
using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;
using Strawhenge.GameManagement.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

public class InitializerScript : MonoBehaviour
{
    static ILogger _logger;
    static DefaultSaveDataFactory _defaultSaveDataFactory;
    static CurrentSaveDataContainer<SaveData> _currentSaveDataContainer;
    static InMemorySaveDataRepository _saveDataRepository;
    static SelectedSaveDataController<SaveData> _saveDataSelectorController;
    static SceneNames _sceneNames;
    static GameManager _gameManager;
    static PauseGame _pauseGame;
    static SaveGameCommandFactory<SaveData> _saveGameCommandFactory;
    static SaveDataGenerator _saveDataGenerator;
    static PlayerState _playerState;

    void Awake()
    {
        _logger ??= GlobalUnityLogger.Instance;

        _defaultSaveDataFactory ??= new DefaultSaveDataFactory();

        _currentSaveDataContainer ??= new CurrentSaveDataContainer<SaveData>(
            _defaultSaveDataFactory);

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

        _playerState ??= new PlayerState();

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
            restartMenu.Player = _playerState;
        }

        if (FindObjectOfType<PostGameSceneLoadedScript>() is PostGameSceneLoadedScript postGameSceneLoaded)
        {
            postGameSceneLoaded.SaveDataState = _saveDataSelectorController;
            postGameSceneLoaded.SceneNames = _sceneNames;
        }

        if (FindObjectOfType<PlayerPositionSegmentScript>() is PlayerPositionSegmentScript playerPositionSegment)
        {
            playerPositionSegment.SaveDataAccessor = _currentSaveDataContainer;
        }

        if (FindObjectOfType<WaitForSecondsSegmentScript>() is WaitForSecondsSegmentScript waitForSecondsSegment)
        {
            waitForSecondsSegment.SaveDataAccessor = _currentSaveDataContainer;
        }

        if (FindObjectOfType<InputScript>() is InputScript input)
        {
            input.PauseGame = _pauseGame;
            input.PlayerState = _playerState;
        }

        PopulateSaveRepository();
    }

    void PopulateSaveRepository()
    {
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

class PlayerState : IPlayerState
{
    public event Action Died;

    public void InvokeDied() => Died?.Invoke();
}

class SaveDataGenerator : ISaveDataGenerator<SaveData>
{
    public SaveData GenerateForCurrentGameState()
    {
        return new SaveData();
    }
}

class DefaultSaveDataFactory : IDefaultSaveDataFactory<SaveData>
{
    public SaveData Create()
    {
        return new SaveData();
    }
}

class InMemorySaveDataRepository : ISaveDataRepository<SaveData>, ISaveMetaDataRepository
{
    readonly Dictionary<Guid, SaveData> _saveDataById = new();
    readonly Dictionary<Guid, SaveMetaData> _saveMetaDataById = new();

    public void Add(SaveData saveData, Guid id, DateTime dateTimeCreated)
    {
        _saveDataById.Add(id, saveData);
        _saveMetaDataById.Add(id, new SaveMetaData(id, dateTimeCreated));
    }

    Task<SaveData> ISaveDataRepository<SaveData>.GetAsync(Guid id)
    {
        return Task.FromResult(_saveDataById[id]);
    }

    Task ISaveDataRepository<SaveData>.DeleteAsync(Guid id)
    {
        _saveDataById.Remove(id);
        _saveMetaDataById.Remove(id);
        return Task.CompletedTask;
    }

    Task ISaveDataRepository<SaveData>.SaveAsync(SaveData saveData)
    {
        var id = Guid.NewGuid();
        _saveDataById.Add(id, saveData);
        _saveMetaDataById.Add(id, new SaveMetaData(id, DateTime.UtcNow));
        return Task.CompletedTask;
    }

    IReadOnlyList<SaveMetaData> ISaveMetaDataRepository.GetAll()
    {
        return _saveMetaDataById.Values.ToArray();
    }

    Maybe<SaveMetaData> ISaveMetaDataRepository.GetMostRecent()
    {
        return _saveMetaDataById.Values
            .OrderByDescending(m => m.DateTimeCreated)
            .FirstOrNone();
    }
}