using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public sealed class LoadGameMenuScript : MonoBehaviour
    {
        [SerializeField] SaveDataMenuScript _saveDataMenu;

        public event Action<SaveMetaData> Load;
        public event Action Back;

        void Start()
        {
            Hide();
        }

        public void Show()
        {
            _saveDataMenu.Show(
                onBackSelected: () => Back?.Invoke(),
                onSaveSelected: save => Load?.Invoke(save));
        }

        public void Hide()
        {
            _saveDataMenu.Hide();
        }
    }
}