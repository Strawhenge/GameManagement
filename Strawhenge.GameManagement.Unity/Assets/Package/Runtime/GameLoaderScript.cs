using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Strawhenge.GameManagement.Unity
{
    public class GameLoaderScript : MonoBehaviour
    {
        [SerializeField] GameLoadSegmentScript[] _segments;

        public ISaveDataState SaveDataState { private get; set; }

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

            UnloadScene("LoadProgress");

            foreach (var segment in _segments)
                segment.BeginLoad();

            while (!_segments.All(x => x.IsLoaded))
            {
                yield return null;
            }

            UnloadScene("LoadingScreen");

            Destroy(gameObject);
            Time.timeScale = 1;
        }

        static void UnloadScene(string name)
        {
            var scene = SceneManager.GetSceneByName(name);

            if (scene.isLoaded)
                SceneManager.UnloadSceneAsync(scene);
        }
    }
}