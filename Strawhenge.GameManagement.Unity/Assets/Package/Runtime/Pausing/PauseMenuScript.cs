using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public sealed class PauseMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;
        [SerializeField] SaveGameMenuScript _saveGameMenu;
        [SerializeField] Canvas _pauseMenuCanvas;
        [SerializeField] Button _continueButton;
        [SerializeField] Button _saveGameButton;
        [SerializeField] Button _loadGameButton;
        [SerializeField] Button _mainMenuButton;
        [SerializeField] Button _quitButton;

        void Awake()
        {
            _continueButton.onClick.AddListener(Continue);
            _saveGameButton.onClick.AddListener(SaveGame);
            _loadGameButton.onClick.AddListener(LoadGame);
            _mainMenuButton.onClick.AddListener(MainMenu);
            _quitButton.onClick.AddListener(Quit);
        }

        void Start()
        {
            _pauseMenuCanvas.enabled = false;
            GameManagement.PauseGame.Paused += OnPause;
            GameManagement.PauseGame.Resumed += OnResume;
        }

        void OnDestroy()
        {
            GameManagement.PauseGame.Paused -= OnPause;
            GameManagement.PauseGame.Resumed -= OnResume;
        }

        void OnPause() => _pauseMenuCanvas.enabled = true;

        void OnResume()
        {
            _pauseMenuCanvas.enabled = false;
            _saveGameMenu.Hide();
            _loadGameMenu.Hide();
        }

        public void Continue()
        {
            GameManagement.PauseGame.Resume();
        }

        public void SaveGame()
        {
            _pauseMenuCanvas.enabled = false;
            _saveGameMenu.Show();
            _saveGameMenu.Back += OnReturnFromSaveGameMenu;
            _saveGameMenu.Saved += OnReturnFromSaveGameMenu;
        }

        void OnReturnFromSaveGameMenu()
        {
            _saveGameMenu.Back -= OnReturnFromSaveGameMenu;
            _saveGameMenu.Saved -= OnReturnFromSaveGameMenu;
            _saveGameMenu.Hide();
            _pauseMenuCanvas.enabled = true;
        }

        public void LoadGame()
        {
            _pauseMenuCanvas.enabled = false;
            _loadGameMenu.Show();
            _loadGameMenu.Load += OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back += OnBackFromLoadGameMenu;
        }

        void OnSaveSelectedFromLoadGameMenu(SaveMetaData save)
        {
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;

            GameManagement.PauseGame.Resume();
            GameManagement.GameManager.LoadSave(save);
        }

        void OnBackFromLoadGameMenu()
        {
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Hide();
            _pauseMenuCanvas.enabled = true;
        }

        public void MainMenu()
        {
            GameManagement.PauseGame.Resume();
            GameManagement.GameManager.MainMenu();
        }

        public void Quit()
        {
            GameManagement.GameManager.Quit();
        }
    }
}