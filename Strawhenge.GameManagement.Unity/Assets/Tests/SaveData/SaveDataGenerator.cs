using Strawhenge.GameManagement.Saving;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.SaveData
{
    class SaveDataGenerator : ISaveDataGenerator<SaveData>
    {
        public SaveData GenerateForCurrentGameState()
        {
            return new SaveData
            {
                PlayerPosition = Player.position,
                SecondsToWait = Random.Range(0, 5)
            };
        }

        public static Rigidbody Player { private get; set; }
    }
}