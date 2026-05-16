using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity.Tests.PlayModeTests
{
    public class ReturnToMainMenuFromGame : BasePlayModeTest
    {
        const int TimeOutInMilliseconds = 10_000;

        protected override IEnumerator Run()
        {
            yield return LoadInitialScene();

            var mainMenu = GetMainMenu();
            mainMenu.NewGame();

            yield return WaitForLoadingScreenTransition();

            var pauseMenu = GetPauseMenu();
            pauseMenu.MainMenu();

            yield return WaitForSceneLoad(MainMenuSceneName);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void MainMenuSceneShouldBeLoaded()
        {
            Assert.True(SceneManager.GetSceneByName(MainMenuSceneName).isLoaded);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void MainMenuSceneShouldBeActive()
        {
            Assert.True(SceneManager.GetActiveScene().name == MainMenuSceneName);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void GameSceneShouldNotBeLoaded()
        {
            Assert.False(SceneManager.GetSceneByName(GameSceneName).isLoaded);
        }
    }
}