using NUnit.Framework;
using Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments;
using Strawhenge.GameManagement.Unity.Tests.SaveData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Strawhenge.GameManagement.Unity.Tests.PlayModeTests
{
    public class LoadSaveFromMainMenu : BasePlayModeTest
    {
        const int TimeOutInMilliseconds = 10_000;

        Guid _saveId;
        Vector3 _playerPosition;

        protected override IEnumerator Run()
        {
            var repository = Object.FindObjectOfType<InMemorySaveDataRepositoryScript>().Repository;
            repository.DeleteAll();

            _saveId = Guid.NewGuid();
            _playerPosition = new Vector3(1, 2, 3);

            repository.Add(
                new SaveData.SaveData
                {
                    PlayerPosition = _playerPosition
                },
                _saveId,
                DateTime.UtcNow);

            yield return LoadInitialScene();

            var mainMenu = GetMainMenu();
            mainMenu.LoadGame();

            var saveDataMenu = GetSaveDataMenu();
            saveDataMenu.Select(0);

            yield return WaitForLoadingScreenTransition();
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void GameSceneShouldBeLoaded()
        {
            Assert.True(SceneManager.GetSceneByName(GameSceneName).isLoaded);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void GameSceneShouldBeActive()
        {
            Assert.True(SceneManager.GetActiveScene().name == GameSceneName);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void SaveShouldBeLoaded()
        {
            Assert.True(StoreSaveDataSegmentScript.SaveData.HasSome(out var saveData));
            Assert.AreEqual(_playerPosition, saveData.PlayerPosition);
        }
    }
}