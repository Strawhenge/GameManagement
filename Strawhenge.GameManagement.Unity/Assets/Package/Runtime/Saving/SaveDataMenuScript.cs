using Strawhenge.GameManagement.SaveRepository;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity.Saving
{
    public sealed class SaveDataMenuScript : MonoBehaviour
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

        void Awake()
        {
            _backButton.onClick.AddListener(() => _onBackSelected?.Invoke());

            _newSaveButton = AddButton(
                _newSaveButtonText,
                onSelect: () => _onNewSaveSelected?.Invoke());
        }

        void Start()
        {
            Hide();
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
        
        public void Select(int index) => _selectSaveButtons[index].GetComponent<Button>().onClick.Invoke();

        void PopulateSaves()
        {
            var saves = GameManager.SaveMetaDataRepository.GetAll();

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