using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.SaveData
{
    public class InMemorySaveDataRepositoryScript : MonoBehaviour
    {
        internal InMemorySaveDataRepository Repository { get; } = new();
    }
}