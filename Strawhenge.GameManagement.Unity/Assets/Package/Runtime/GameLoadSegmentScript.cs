using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public abstract class GameLoadSegmentScript : MonoBehaviour
    {
        public abstract bool IsLoaded { get; }

        public abstract void BeginLoad();
    }
}