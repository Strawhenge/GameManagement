using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Setup
{
    [Serializable]
    public class SerializedSceneName
    {
        internal static string NameFieldName => nameof(_name);
        
        [SerializeField] string _name;

        public string Name => _name;
    }
}