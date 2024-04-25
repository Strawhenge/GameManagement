using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity
{
    public class LoadingScreenScript : MonoBehaviour
    {
        public ISelectedSaveDataLoader SelectedSaveDataLoader { private get; set; }
        
        void Start()
        {
            StartCoroutine(LoadMainScene());
            StartCoroutine(LoadProgress());
        }

        IEnumerator LoadMainScene()
        {
            yield return new WaitForEndOfFrame();

            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive).completed += _ =>
            {
                SceneManager.SetActiveScene(
                    SceneManager.GetSceneByName("Main"));
            };
        }
        
        IEnumerator LoadProgress()
        {
            var task = SelectedSaveDataLoader.LoadProgress();
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