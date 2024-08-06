using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public class LoadGameMenuScript : MonoBehaviour
    {
        [SerializeField] SaveDataMenuScript _saveDataMenu;

        public event Action<SaveMetaData> Load;
        public event Action Back;

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