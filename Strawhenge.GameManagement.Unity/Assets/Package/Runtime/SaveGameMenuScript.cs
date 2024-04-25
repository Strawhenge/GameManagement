using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveGameMenuScript : MonoBehaviour
    {
        [SerializeField] SaveDataMenuScript _saveDataMenu;
        [SerializeField] SavingScript _saving;

        Action _onNewSaveSelectedStrategy;

        public event Action Saved;
        public event Action Back;

        void Awake()
        {
            if (_saveDataMenu == null)
            {
                Debug.LogError($"'{nameof(_saving)}' is missing.", this);
                return;
            }

            _saveDataMenu.BackSelected += OnBack;
            _saveDataMenu.SaveSelected += OnSaveSelected;

            _onNewSaveSelectedStrategy = () => Save();
            _saveDataMenu.AddPermanentButton("New Save", () => _onNewSaveSelectedStrategy());
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

        void Save(SaveMetaData saveToOverwrite = null)
        {
            _saveDataMenu.BackSelected -= OnBack;
            _saveDataMenu.SaveSelected -= OnSaveSelected;
            _onNewSaveSelectedStrategy = () => { };

            _saving.Save(
                onSafeToReturnToGameplay: () =>
                {
                    _saveDataMenu.BackSelected += OnBack;
                    Saved?.Invoke();
                },
                onCompleted: () =>
                {
                    _saveDataMenu.SaveSelected += OnSaveSelected;
                    _onNewSaveSelectedStrategy = () => Save();
                },
                saveToOverwrite);
        }

        void OnSaveSelected(SaveMetaData save) => Save(save);

        void OnBack() => Back?.Invoke();
    }
}