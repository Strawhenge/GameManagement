using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.SaveData
{
    [Serializable]
    public class SerializedSaveData
    {
        [SerializeField] string _dateTimeCreated = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        [SerializeField] Vector3 _playerPosition;
        [SerializeField] int _secondsToWait;

        public string DateTimeCreated => _dateTimeCreated;

        public Vector3 PlayerPosition => _playerPosition;

        public int SecondsToWait => _secondsToWait;
    }
}