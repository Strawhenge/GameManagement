using System.Collections;
using System.Linq;
using Strawhenge.GameManagement.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity
{
    public class PostGameSceneLoadedScript : MonoBehaviour
    {
        [SerializeField] PostGameSceneLoadedSegmentScript[] _segments;

        public ISelectedSaveDataState SaveDataState { private get; set; }

        public ISceneNames SceneNames { private get; set; }

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
            while (SaveDataState.IsAwaitingSelectedSaveDataLoad)
            {
                yield return null;
            }

            foreach (var segment in _segments)
                segment.Run();

            while (!_segments.All(x => x.IsCompleted))
            {
                yield return null;
            }

            SceneManager.UnloadSceneAsync(SceneNames.LoadingScreen);

            Destroy(gameObject);
            Time.timeScale = 1;
        }
    }
}