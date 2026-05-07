using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public class SettingsScriptableObject : ScriptableObject
    {
        [SerializeField] BaseGameManagerScript _gameManagerPrefab;

        public BaseGameManagerScript GameManagerPrefab => _gameManagerPrefab;
    }
}