using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public class StartNewGameFromMainMenu
    {
        bool _setupRan;
        bool _loadingScreenSceneLoadCompleted;

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
        }

        [Test]
        public void VerifyGameSceneLoaded()
        {
            Assert.True(SceneManager.GetSceneByName("Game").isLoaded);
        }

        [Test]
        public void VerifyGameSceneActive()
        {
            Assert.True(SceneManager.GetActiveScene().name == "Game");
        }
    }
}