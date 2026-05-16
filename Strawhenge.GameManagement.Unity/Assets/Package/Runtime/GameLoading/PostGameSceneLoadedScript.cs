using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity.GameLoading
{
    public sealed class PostGameSceneLoadedScript : MonoBehaviour
    {
        [SerializeField] PostGameSceneLoadedSegmentScript[] _segments;
        [SerializeField] UnityEvent _completed;

        void Awake()
        {
            Time.timeScale = 0;
        }

        void Start()
        {
            StartCoroutine(Load());
        }

        IEnumerator Load()
        {
            while (GameManager.SelectedSaveDataController.IsAwaitingSelectedSaveDataLoad)
            {
                yield return null;
            }

            GameManager.ClearSaveSaveGeneratorSteps.Clear();

            foreach (var segment in _segments)
                segment.Run();

            while (!_segments.All(x => x.IsCompleted))
            {
                yield return null;
            }

            var loadingScreen = SceneManager.GetSceneByName(GameManager.SceneNames.LoadingScreen);
            if (loadingScreen.isLoaded)
                SceneManager.UnloadSceneAsync(GameManager.SceneNames.LoadingScreen);

            _completed.Invoke();
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}