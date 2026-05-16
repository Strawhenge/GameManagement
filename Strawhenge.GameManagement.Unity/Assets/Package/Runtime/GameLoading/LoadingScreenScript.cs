using System.Collections;
using Strawhenge.GameManagement.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity
{
    public sealed class LoadingScreenScript : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(LoadMainScene());
            StartCoroutine(LoadProgress());
        }

        IEnumerator LoadMainScene()
        {
            yield return new WaitForEndOfFrame();

            var loadGameScene = SceneManager
                .LoadSceneAsync(GameManager.SceneNames.Game, LoadSceneMode.Additive);

            if (loadGameScene == null)
            {
                Debug.LogError("Could not load game scene.", this);
                yield break;
            }

            loadGameScene.completed += _ =>
            {
                SceneManager.SetActiveScene(
                    SceneManager.GetSceneByName(GameManager.SceneNames.Game));
            };
        }

        IEnumerator LoadProgress()
        {
            yield return new WaitForEndOfFrame();

            var task = GameManager.SelectedSaveDataLoader.LoadProgress();
            yield return new WaitUntil(() => task.IsCompleted);

            if (task.IsFaulted)
            {
                Debug.LogError("Failed to load progress.", this);

                if (task.Exception != null)
                    foreach (var exception in task.Exception.InnerExceptions)
                        Debug.LogException(exception, this);
            }
        }
    }
}