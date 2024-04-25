using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveDataMenuScript : MonoBehaviour
    {
        [SerializeField] Button _backButton;
        [SerializeField] Transform _selectSaveButtonParent;
        [SerializeField] Button _selectSaveButtonPrefab;

        readonly List<GameObject> _selectSaveButtons = new List<GameObject>();

        public event Action<SaveMetaData> SaveSelected;

        public event Action BackSelected;

        public ISaveMetaDataRepository SaveMetaDataRepository { private get; set; }

        void Awake()
        {
            _backButton.onClick.AddListener((() => BackSelected?.Invoke()));
        }

        public void Show()
        {
            gameObject.SetActive(true);
            PopulateSaves();
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            foreach (var button in _selectSaveButtons)
                Destroy(button);

            _selectSaveButtons.Clear();
        }

        public void AddPermanentButton(string text, Action onSelect) => AddButton(text, onSelect);

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