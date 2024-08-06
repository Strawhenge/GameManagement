using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveDataMenuScript : MonoBehaviour
    {
        [SerializeField] string _newSaveButtonText = "New Save";
        [SerializeField] Canvas _canvas;
        [SerializeField] Button _backButton;
        [SerializeField] Transform _selectSaveButtonParent;
        [SerializeField] Button _selectSaveButtonPrefab;

        readonly List<GameObject> _selectSaveButtons = new List<GameObject>();
        GameObject _newSaveButton;

        Action<SaveMetaData> _onSaveSelected;
        Action _onBackSelected;
        Action _onNewSaveSelected;

        public ISaveMetaDataRepository SaveMetaDataRepository { private get; set; }

        void Awake()
        {
            _backButton.onClick.AddListener(() => _onBackSelected?.Invoke());

            _newSaveButton = AddButton(
                _newSaveButtonText,
                onSelect: () => _onNewSaveSelected?.Invoke());
        }

        public void Show(
            Action onBackSelected,
            Action<SaveMetaData> onSaveSelected,
            Action onNewSaveSelected = null)
        {
            _onBackSelected = onBackSelected;
            _onSaveSelected = onSaveSelected;

            _onNewSaveSelected = onNewSaveSelected;
            _newSaveButton?.SetActive(_onNewSaveSelected != null);

            _canvas.enabled = true;
            PopulateSaves();
        }

        public void Hide()
        {
            _onBackSelected = null;
            _onSaveSelected = null;
            _onNewSaveSelected = null;

            _canvas.enabled = false;

            foreach (var button in _selectSaveButtons)
                Destroy(button);

            _selectSaveButtons.Clear();
        }

        void PopulateSaves()
        {
            var saves = SaveMetaDataRepository.GetAll();

            foreach (var save in saves)
            {
                var button = AddButton(
                    save.DateTimeCreated.ToString("HH:mm:ss dd/MM/yyyy"),
                    () => _onSaveSelected?.Invoke(save));

                _selectSaveButtons.Add(button.gameObject);
            }
        }

        GameObject AddButton(string text, Action onSelect)
        {
            var button = Instantiate(_selectSaveButtonPrefab, _selectSaveButtonParent);
            button.GetComponentInChildren<Text>().text = text;
            button.onClick.AddListener(() => onSelect());
            return button.gameObject;
        }
    }
}