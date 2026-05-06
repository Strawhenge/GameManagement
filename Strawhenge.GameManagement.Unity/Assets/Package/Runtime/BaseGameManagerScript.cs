using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public abstract class BaseGameManagerScript : MonoBehaviour
    {
        protected internal abstract void RunSetup();
    }

    public abstract class BaseGameManagerScript<TSaveData> : BaseGameManagerScript
    {
        protected internal sealed override void RunSetup()
        {
        }
    }
}