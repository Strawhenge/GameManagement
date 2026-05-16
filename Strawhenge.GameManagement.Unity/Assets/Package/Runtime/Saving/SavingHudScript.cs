using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Saving
{
    public sealed class SavingHudScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;

        void Awake()
        {
            Hide();
            GameManager.SaveGame.SaveStarted += Show;
            GameManager.SaveGame.SaveCompleted += Hide;
        }

        void OnDestroy()
        {
            GameManager.SaveGame.SaveStarted -= Show;
            GameManager.SaveGame.SaveCompleted -= Hide;
        }

        void Show() => _canvas.enabled = true;

        void Hide() => _canvas.enabled = false;
    }
}