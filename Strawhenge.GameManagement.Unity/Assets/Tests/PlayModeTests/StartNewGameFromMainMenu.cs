using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity.Tests.PlayModeTests
{
    public class StartNewGameFromMainMenu : BasePlayModeTest
    {
        protected override IEnumerator Run()
        {
            yield return LoadInitialScene();

            var mainMenu = GetMainMenu();
            mainMenu.NewGame();

            yield return WaitForLoadingScreenTransition();
        }

        [Test]
        public void GameSceneShouldBeLoaded()
        {
            Assert.True(SceneManager.GetSceneByName(GameSceneName).isLoaded);
        }

        [Test]
        public void GameSceneShouldBeActive()
        {
            Assert.True(SceneManager.GetActiveScene().name == GameSceneName);
        }
    }
}