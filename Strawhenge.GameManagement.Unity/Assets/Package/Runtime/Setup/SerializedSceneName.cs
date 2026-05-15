using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    [Serializable]
    public class SerializedSceneName
    {
        internal static string NameFieldName => nameof(_name);
        
        [SerializeField] string _name;

        public string Name => _name;
    }
}