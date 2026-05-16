using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Strawhenge.GameManagement.Unity.Tests.PlayModeTests
{
    public abstract class BasePlayModeTest
    {
        protected const string MainMenuSceneName = "Main Menu";
        protected const string LoadingScreenSceneName = "Loading Screen";
        protected const string GameSceneName = "Game";

        bool _mainMenuSceneLoaded;
        bool _loadingScreenSceneLoaded;
        bool _gameSceneLoaded;
        bool _setupRan;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            if (_setupRan) yield break;
            _setupRan = true;

            SceneManager.sceneLoaded += OnSceneLoaded;

            yield return Run();
        }

        protected abstract IEnumerator Run();

        protected IEnumerator LoadInitialScene()
        {
            yield return SceneManager.LoadSceneAsync(0);
        }

        protected IEnumerator WaitForLoadingScreenTransition()
        {
            yield return new WaitUntil(() =>
                _loadingScreenSceneLoaded &&
                !SceneManager.GetSceneByName(LoadingScreenSceneName).isLoaded);
        }

        protected IEnumerator WaitForSceneLoad(string sceneName)
        {
            yield return new WaitUntil(() => IsSceneLoaded(sceneName));
        }

        protected MainMenuScript GetMainMenu()
        {
            var mainMenu = Object.FindObjectOfType<MainMenuScript>();
            if (mainMenu == null)
                Assert.Fail($"{nameof(MainMenuScript)} not found.");
            
            return mainMenu;
        }
        
        protected PauseMenuScript GetPauseMenu()
        {
            var pauseMenu = Object.FindObjectOfType<PauseMenuScript>();
            if (pauseMenu == null)
                Assert.Fail($"{nameof(PauseMenuScript)} not found.");
            
            return pauseMenu;
        }
        
        protected SaveDataMenuScript GetSaveDataMenu()
        {
            var saveDataMenu = Object.FindObjectOfType<SaveDataMenuScript>();
            if (saveDataMenu == null)
                Assert.Fail($"{nameof(SaveDataMenuScript)} not found.");
            
            return saveDataMenu;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            switch (scene.name)
            {
                case MainMenuSceneName:
                    _mainMenuSceneLoaded = true;
                    break;
                case LoadingScreenSceneName:
                    _loadingScreenSceneLoaded = true;
                    break;
                case GameSceneName:
                    _gameSceneLoaded = true;
                    break;
            }
        }

        bool IsSceneLoaded(string sceneName)
        {
            switch (sceneName)
            {
                case MainMenuSceneName:
                    return _mainMenuSceneLoaded;
                case LoadingScreenSceneName:
                    return _loadingScreenSceneLoaded;
                case GameSceneName:
                    return _gameSceneLoaded;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, "Unknown scene.");
            }
        }
    }
}