using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests
{
    public static class Startup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Start()
        {
            GameManagementSetup.Run("Settings");
        }
    }
}