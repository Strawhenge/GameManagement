using Strawhenge.GameManagement.Unity;
using System;
using UnityEngine;

public class InitializerScript : MonoBehaviour
{
    static bool _isSaveRepositoryPopulated;

    void Awake()
    {
        if (FindObjectOfType<MainMenuScript>() is MainMenuScript mainMenu)
        {
            mainMenu.GameManager = GameManagement.GameManager;
            mainMenu.SaveMetaDataRepository = GameManagement.SaveMetaDataRepository;
        }

        if (FindObjectOfType<SaveDataMenuScript>() is SaveDataMenuScript saveDataMenu)
        {
            saveDataMenu.SaveMetaDataRepository = GameManagement.SaveMetaDataRepository;
        }

        if (FindObjectOfType<LoadingScreenScript>() is LoadingScreenScript loadingScreen)
        {
            loadingScreen.SelectedSaveDataLoader = GameManagement.SelectedSaveDataLoader;
            loadingScreen.SceneNames = GameManagement.SceneNames;
        }

        if (FindObjectOfType<PauseMenuScript>() is PauseMenuScript pauseMenu)
        {
            pauseMenu.GameManager = GameManagement.GameManager;
            pauseMenu.PauseGame = GameManagement.PauseGame;
        }

        if (FindObjectOfType<SavingScript>() is SavingScript saving)
        {
            saving.SaveGameCommandFactory = GameManagement.SaveCommandFactory;
        }

        if (FindObjectOfType<RestartMenuScript>() is RestartMenuScript restartMenu)
        {
            restartMenu.GameManager = GameManagement.GameManager;
            restartMenu.SaveMetaDataRepository = GameManagement.SaveMetaDataRepository;
            restartMenu.RestartGame = GameManagement.RestartGame;
        }

        if (FindObjectOfType<PostGameSceneLoadedScript>() is PostGameSceneLoadedScript postGameSceneLoaded)
        {
            postGameSceneLoaded.SaveDataState = GameManagement.SelectedSaveDataState;
            postGameSceneLoaded.SceneNames = GameManagement.SceneNames;
        }

        if (FindObjectOfType<PlayerPositionSegmentScript>() is PlayerPositionSegmentScript playerPositionSegment)
        {
            playerPositionSegment.SaveDataAccessor = GameManagement<SaveData>.CurrentSaveDataAccessor;
        }

        if (FindObjectOfType<WaitForSecondsSegmentScript>() is WaitForSecondsSegmentScript waitForSecondsSegment)
        {
            waitForSecondsSegment.SaveDataAccessor = GameManagement<SaveData>.CurrentSaveDataAccessor;
        }

        if (FindObjectOfType<InputScript>() is InputScript input)
        {
            input.PauseGame = GameManagement.PauseGame;
            input.RestartGame = GameManagement.RestartGame;
        }

        PopulateSaveRepository();
    }

    void PopulateSaveRepository()
    {
        if (_isSaveRepositoryPopulated) return;
        _isSaveRepositoryPopulated = true;

        // TODO Change this.
        var saveDataRepository = GameManagement.SaveMetaDataRepository as InMemorySaveDataRepository;

        saveDataRepository!.Add(
            new SaveData
            {
                PlayerPosition = Vector3.zero,
                SecondsToWait = 0
            },
            Guid.NewGuid(),
            DateTime.UtcNow.AddSeconds(-5));

        saveDataRepository.Add(
            new SaveData
            {
                PlayerPosition = new Vector3(0, 10, 0),
                SecondsToWait = 3
            },
            Guid.NewGuid(),
            DateTime.UtcNow.AddHours(-1));

        saveDataRepository.Add(
            new SaveData
            {
                PlayerPosition = new Vector3(4, 0, 0),
                SecondsToWait = 8
            },
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(-2));
    }
}