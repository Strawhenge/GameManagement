using FunctionalUtilities;
using Strawhenge.Common.Unity;
using Strawhenge.GameManagement;
using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;
using Strawhenge.GameManagement.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

public class InitializerScript : MonoBehaviour
{
    static ILogger _logger;
    static DefaultSaveDataFactory _defaultSaveDataFactory;
    static CurrentSaveDataContainer<SaveData> _currentSaveDataContainer;
    static SaveDataRepository _saveDataRepository;
    static SelectedSaveDataController<SaveData> _saveDataSelectorController;
    static SceneNames _sceneNames;
    static GameManager _gameManager;
    static SaveMetaDataRepository _saveMetaDataRepository;
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

        _saveDataRepository ??= new SaveDataRepository();

        _saveDataSelectorController ??= new SelectedSaveDataController<SaveData>(
            _currentSaveDataContainer,
            _saveDataRepository);

        _sceneNames ??= new SceneNames();

        _gameManager ??= new GameManager(
            _saveDataSelectorController,
            _sceneNames,
            _logger);

        _saveMetaDataRepository ??= new SaveMetaDataRepository();

        _pauseGame = new PauseGame(_logger);

        _saveDataGenerator ??= new SaveDataGenerator();

        _saveGameCommandFactory ??= new SaveGameCommandFactory<SaveData>(
            _saveDataGenerator,
            _saveDataRepository);

        _playerState ??= new PlayerState();

        if (FindObjectOfType<MainMenuScript>() is MainMenuScript mainMenu)
        {
            mainMenu.GameManager = _gameManager;
            mainMenu.SaveMetaDataRepository = _saveMetaDataRepository;
        }

        if (FindObjectOfType<SaveDataMenuScript>() is SaveDataMenuScript saveDataMenu)
        {
            saveDataMenu.SaveMetaDataRepository = _saveMetaDataRepository;
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
            restartMenu.SaveMetaDataRepository = _saveMetaDataRepository;
            restartMenu.Player = _playerState;
        }
    }
}

class PlayerState : IPlayerState
{
    public event Action Died;
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

class SaveDataRepository : ISaveDataRepository<SaveData>
{
    public Task<SaveData> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(SaveData saveData)
    {
        throw new NotImplementedException();
    }
}

class SaveMetaDataRepository : ISaveMetaDataRepository
{
    public IReadOnlyList<SaveMetaData> GetAll() => Array.Empty<SaveMetaData>();

    public Maybe<SaveMetaData> GetMostRecent() => Maybe.None<SaveMetaData>();
}