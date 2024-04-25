using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public abstract class PostGameSceneLoadedSegmentScript : MonoBehaviour
    {
        public abstract bool IsCompleted { get; }

        public abstract void Run();
    }
}