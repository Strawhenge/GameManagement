using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Strawhenge.GameManagement.Unity.Tests.PlayModeTests
{
    public class ReturnToMainMenuFromGame
    {
        const int TimeOutInMilliseconds = 10_000;

        bool _setupRan;
        bool _loadingScreenSceneLoadCompleted;
        bool _mainMenuSceneLoadCompleted;

        [UnitySetUp]
        public IEnumerator Run()
        {
            if (_setupRan) yield break;
            _setupRan = true;

            SceneManager.sceneLoaded += (scene, _) =>
            {
                if (scene.name == "Loading Screen")
                    _loadingScreenSceneLoadCompleted = true;
            };

            yield return SceneManager.LoadSceneAsync(0);

            var mainMenu = Object.FindObjectOfType<MainMenuScript>();
            mainMenu.NewGame();

            yield return new WaitUntil(() =>
                _loadingScreenSceneLoadCompleted && !SceneManager.GetSceneByName("Loading Screen").isLoaded);

            SceneManager.sceneLoaded += (scene, _) =>
            {
                if (scene.name == "Main Menu")
                    _mainMenuSceneLoadCompleted = true;
            };

            var pauseMenu = Object.FindObjectOfType<PauseMenuScript>();
            pauseMenu.MainMenu();

            yield return new WaitUntil(() => _mainMenuSceneLoadCompleted);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void VerifyMainMenuSceneLoaded()
        {
            Assert.True(SceneManager.GetSceneByName("Main Menu").isLoaded);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void VerifyMainMenuSceneActive()
        {
            Assert.True(SceneManager.GetActiveScene().name == "Main Menu");
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void VerifyGameSceneNotLoaded()
        {
            Assert.False(SceneManager.GetSceneByName("Game").isLoaded);
        }
    }
}