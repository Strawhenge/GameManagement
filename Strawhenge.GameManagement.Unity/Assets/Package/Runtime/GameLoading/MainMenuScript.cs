using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
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
            var mostRecentSave = GameManagement.SaveMetaDataRepository.GetMostRecent();

            mostRecentSave.Do(
                GameManagement.GameManager.LoadSave);
        }

        public void NewGame()
        {
            GameManagement.GameManager.StartNewGame();
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
            GameManagement.GameManager.Quit();
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

            GameManagement.GameManager.LoadSave(save);
        }
    }
}