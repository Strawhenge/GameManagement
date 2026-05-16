using NUnit.Framework;
using Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments;
using Strawhenge.GameManagement.Unity.Tests.SaveData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Strawhenge.GameManagement.Unity.Tests.PlayModeTests
{
    public class LoadSaveFromMainMenu
    {
        const int TimeOutInMilliseconds = 10_000;

        bool _setupRan;
        bool _loadingScreenSceneLoadCompleted;
        Guid _saveId;
        Vector3 _playerPosition;

        [UnitySetUp]
        public IEnumerator Run()
        {
            if (_setupRan) yield break;
            _setupRan = true;

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

            SceneManager.sceneLoaded += (scene, _) =>
            {
                if (scene.name == "Loading Screen")
                    _loadingScreenSceneLoadCompleted = true;
            };

            yield return SceneManager.LoadSceneAsync(0);

            var mainMenu = Object.FindObjectOfType<MainMenuScript>();
            mainMenu.LoadGame();

            var saveDataMenu = Object.FindObjectOfType<SaveDataMenuScript>();
            saveDataMenu.Select(0);

            yield return new WaitUntil(() =>
                _loadingScreenSceneLoadCompleted && !SceneManager.GetSceneByName("Loading Screen").isLoaded);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void VerifyGameSceneLoaded()
        {
            Assert.True(SceneManager.GetSceneByName("Game").isLoaded);
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void VerifyGameSceneActive()
        {
            Assert.True(SceneManager.GetActiveScene().name == "Game");
        }

        [Test, Timeout(TimeOutInMilliseconds)]
        public void VerifySaveIsLoaded()
        {
            Assert.True(StoreSaveDataSegmentScript.SaveData.HasSome(out var saveData));
            Assert.AreEqual(_playerPosition, saveData.PlayerPosition);
        }
    }
}