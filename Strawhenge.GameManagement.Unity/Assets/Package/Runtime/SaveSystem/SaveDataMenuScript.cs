using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveDataMenuScript : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] Button _backButton;
        [SerializeField] Transform _selectSaveButtonParent;
        [SerializeField] Button _selectSaveButtonPrefab;

        readonly List<GameObject> _selectSaveButtons = new List<GameObject>();
        GameObject _newSaveButton;

        public event Action<SaveMetaData> SaveSelected;

        public event Action BackSelected;

        public ISaveMetaDataRepository SaveMetaDataRepository { private get; set; }

        void Awake()
        {
            _backButton.onClick.AddListener((() => BackSelected?.Invoke()));
        }

        public void Show(bool showNewSaveButton = false)
        {
            _newSaveButton?.SetActive(showNewSaveButton);
            _canvas.enabled = true;
            PopulateSaves();
        }

        public void Hide()
        {
            _canvas.enabled = false;

            foreach (var button in _selectSaveButtons)
                Destroy(button);

            _selectSaveButtons.Clear();
        }

        public void AddNewSaveButton(string text, Action onSelect)
        {
            _newSaveButton = AddButton(text, onSelect);
        }

        void PopulateSaves()
        {
            var saves = SaveMetaDataRepository.GetAll();

            foreach (var save in saves)
            {
                var button = AddButton(
                    save.DateTimeCreated.ToString("HH:mm:ss dd/MM/yyyy"),
                    () => SaveSelected?.Invoke(save));

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