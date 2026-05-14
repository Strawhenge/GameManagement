using System;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveGameMenuScript : MonoBehaviour
    {
        [SerializeField] SaveDataMenuScript _saveDataMenu;
        [SerializeField] SavingScript _saving;

        Action _onBackSelectedStrategy;
        Action<SaveMetaData> _onSaveSelectedStrategy;
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

            _onBackSelectedStrategy = OnBack;
            _onSaveSelectedStrategy = OnSaveSelected;
            _onNewSaveSelectedStrategy = () => Save();
        }

        public void Show()
        {
            _saveDataMenu.Show(
                onBackSelected: () => _onBackSelectedStrategy(),
                onSaveSelected: save => _onSaveSelectedStrategy(save),
                onNewSaveSelected: () => _onNewSaveSelectedStrategy());
        }

        public void Hide()
        {
            _saveDataMenu.Hide();
        }

        void Save(SaveMetaData saveToOverwrite = null)
        {
            _onBackSelectedStrategy = () => { };
            _onSaveSelectedStrategy = _ => { };
            _onNewSaveSelectedStrategy = () => { };

            GameManagement.SaveGame.SaveSafeToReturnToGameplay += OnSaveSaveToReturnToGameplay;
            GameManagement.SaveGame.Save(saveToOverwrite);
            Saved?.Invoke();
        }

        void OnSaveSelected(SaveMetaData save) => Save(save);

        void OnBack() => Back?.Invoke();
        
        void OnSaveSaveToReturnToGameplay()
        {
            GameManagement.SaveGame.SaveSafeToReturnToGameplay -= OnSaveSaveToReturnToGameplay;
            _onBackSelectedStrategy = OnBack;
            _onSaveSelectedStrategy = OnSaveSelected;
            _onNewSaveSelectedStrategy = () => Save();
        }
    }
}