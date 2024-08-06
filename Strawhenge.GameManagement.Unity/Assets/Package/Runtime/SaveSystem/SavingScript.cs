using Strawhenge.GameManagement.Saving;
using System;
using System.Collections;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public class SavingScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        bool _inProgress;

        public ISaveGameCommandFactory SaveGameCommandFactory { private get; set; }

        void Awake()
        {
            _canvas.enabled = false;
        }

        public void Save(
            Action onSafeToReturnToGameplay,
            Action onCompleted,
            SaveMetaData saveToOverwrite = null)
        {
            if (_inProgress)
            {
                Debug.LogError("Saving already in progress.");
                return;
            }

            _inProgress = true;
            _canvas.enabled = true;

            StartCoroutine(
                PerformSave(onSafeToReturnToGameplay, onCompleted, saveToOverwrite));
        }

        IEnumerator PerformSave(
            Action onSafeToReturnToGameplay,
            Action onCompleted,
            SaveMetaData saveToOverwrite = null)
        {
            var command = SaveGameCommandFactory.Create(saveToOverwrite);

            onSafeToReturnToGameplay();

            var task = command.SaveAsync();
            yield return new WaitUntil(() => task.IsCompleted);

            _inProgress = false;
            _canvas.enabled = false;
            onCompleted();
        }
    }
}