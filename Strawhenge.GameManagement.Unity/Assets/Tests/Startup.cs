using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.Tests
{
    public static class Startup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Start()
        {
            GameManagementSetup.Run();
        }
    }
}