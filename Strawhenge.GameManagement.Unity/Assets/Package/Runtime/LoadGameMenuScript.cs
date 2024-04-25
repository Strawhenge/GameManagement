using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public class LoadGameMenuScript : MonoBehaviour
    {
        [SerializeField] SaveDataMenuScript _saveDataMenu;

        public event Action<SaveMetaData> Load;
        public event Action Back;

        void Awake()
        {
            _saveDataMenu.BackSelected += () => Back?.Invoke();
            _saveDataMenu.SaveSelected += save => Load?.Invoke(save);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _saveDataMenu.Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _saveDataMenu.Hide();
        }
    }
}