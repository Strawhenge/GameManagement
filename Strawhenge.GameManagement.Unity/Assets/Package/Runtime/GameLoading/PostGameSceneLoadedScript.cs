using System.Collections;
using System.Linq;
using Strawhenge.GameManagement.Loading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity
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
            while (GameManagement.SelectedSaveDataState.IsAwaitingSelectedSaveDataLoad)
            {
                yield return null;
            }

            GameManagement.ClearSaveSaveGeneratorSteps.Clear();

            foreach (var segment in _segments)
                segment.Run();

            while (!_segments.All(x => x.IsCompleted))
            {
                yield return null;
            }

            var loadingScreen = SceneManager.GetSceneByName(GameManagement.SceneNames.LoadingScreen);
            if (loadingScreen.isLoaded)
                SceneManager.UnloadSceneAsync(GameManagement.SceneNames.LoadingScreen);

            _completed.Invoke();
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}