using FunctionalUtilities;
using Strawhenge.Common.Logging;
using Strawhenge.GameManagement;
using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InitializerScript : MonoBehaviour
{
    static DefaultSaveDataFactory _defaultSaveDataFactory;
    static CurrentSaveDataContainer<SaveData> _currentSaveDataContainer;
    static SaveDataRepository _saveDataRepository;
    static SelectedSaveDataController<SaveData> _saveDataSelectorController;
    static GameManager _gameManager;
    static SaveMetaDataRepository _saveMetaDataRepository;

    void Awake()
    {
        _defaultSaveDataFactory ??= new DefaultSaveDataFactory();

        _currentSaveDataContainer ??= new CurrentSaveDataContainer<SaveData>(
            _defaultSaveDataFactory);

        _saveDataRepository ??= new SaveDataRepository();

        _saveDataSelectorController ??= new SelectedSaveDataController<SaveData>(
            _currentSaveDataContainer,
            _saveDataRepository);

        _gameManager ??= new GameManager(
            _saveDataSelectorController,
            new SceneNames(),
            NullLogger.Instance);

        _saveMetaDataRepository ??= new SaveMetaDataRepository();

        if (FindObjectOfType<MainMenuScript>() is MainMenuScript mainMenu)
        {
            mainMenu.GameManager = _gameManager;
            mainMenu.SaveMetaDataRepository = _saveMetaDataRepository;
        }

        if (FindObjectOfType<SaveDataMenuScript>() is SaveDataMenuScript saveDataMenu)
        {
            saveDataMenu.SaveMetaDataRepository = _saveMetaDataRepository;
        }
    }
}

public class DefaultSaveDataFactory : IDefaultSaveDataFactory<SaveData>
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