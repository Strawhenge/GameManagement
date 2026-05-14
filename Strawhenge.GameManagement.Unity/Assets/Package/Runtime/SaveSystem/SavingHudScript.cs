using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public sealed class SavingHudScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;

        void Awake()
        {
            Hide();
            GameManagement.SaveGame.SaveStarted += Show;
            GameManagement.SaveGame.SaveCompleted += Hide;
        }

        void OnDestroy()
        {
            GameManagement.SaveGame.SaveStarted -= Show;
            GameManagement.SaveGame.SaveCompleted -= Hide;
        }

        void Show() => _canvas.enabled = true;

        void Hide() => _canvas.enabled = false;
    }
}