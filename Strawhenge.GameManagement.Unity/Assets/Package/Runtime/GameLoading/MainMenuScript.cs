using Strawhenge.GameManagement.SaveRepository;
using Strawhenge.GameManagement.Unity.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity.GameLoading
{
    public sealed class MainMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;
        [SerializeField] Canvas _canvas;
        [SerializeField] Button _continueButton;
        [SerializeField] Button _newGameButton;
        [SerializeField] Button _loadGameButton;
        [SerializeField] Button _quitButton;

        void Awake()
        {
            _continueButton.onClick.AddListener(Continue);
            _newGameButton.onClick.AddListener(NewGame);
            _loadGameButton.onClick.AddListener(LoadGame);
            _quitButton.onClick.AddListener(Quit);
        }

        public void Continue()
        {
            var mostRecentSave = GameManager.SaveMetaDataRepository.GetMostRecent();

            mostRecentSave.Do(
                GameManager.Flow.LoadSave);
        }

        public void NewGame()
        {
            GameManager.Flow.StartNewGame();
        }

        public void LoadGame()
        {
            _canvas.enabled = false;
            _loadGameMenu.Show();
            _loadGameMenu.Back += OnBackFromLoadGameMenu;
            _loadGameMenu.Load += OnSaveSelectedFromLoadGameMenu;
        }

        public void Quit()
        {
            GameManager.Flow.Quit();
        }

        void OnBackFromLoadGameMenu()
        {
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Hide();
            _canvas.enabled = true;
        }

        void OnSaveSelectedFromLoadGameMenu(SaveMetaData save)
        {
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;

            GameManager.Flow.LoadSave(save);
        }
    }
}