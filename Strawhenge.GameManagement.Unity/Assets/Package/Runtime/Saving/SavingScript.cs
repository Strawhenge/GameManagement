using Strawhenge.GameManagement.Saving;
using System;
using System.Collections;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Saving
{
    public sealed class SavingScript : MonoBehaviour
    {
        internal void Save(ISaveGameCommand command, Action onCompleted)
        {
            StartCoroutine(PerformSave());

            IEnumerator PerformSave()
            {
                var task = command.SaveAsync();
                yield return new WaitUntil(() => task.IsCompleted);

                onCompleted();
            }
        }
    }
}