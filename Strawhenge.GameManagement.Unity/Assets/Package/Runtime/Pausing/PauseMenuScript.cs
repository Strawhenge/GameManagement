using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class PauseMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;
        [SerializeField] SaveGameMenuScript _saveGameMenu;
        [SerializeField] Canvas _pauseMenuCanvas;
        [SerializeField] Button _continueButton;
        [SerializeField] Button _saveGameButton;
        [SerializeField] Button _loadGameButton;
        [SerializeField] Button _mainMenuButton;
        [SerializeField] Button _quitButton;

        public IGameManager GameManager { private get; set; }

        public IPauseGame PauseGame { private get; set; }

        void Awake()
        {
            _saveGameMenu.Hide();
            _loadGameMenu.Hide();

            _continueButton.onClick.AddListener(OnContinueButtonSelected);
            _saveGameButton.onClick.AddListener(OnSaveGameButtonSelected);
            _loadGameButton.onClick.AddListener(OnLoadGameButtonSelected);
            _mainMenuButton.onClick.AddListener(OnMainMenu);
            _quitButton.onClick.AddListener(OnQuitButtonSelected);
        }

        void Start()
        {
            _pauseMenuCanvas.enabled = false;
            PauseGame.Paused += OnPause;
            PauseGame.Resumed += OnResume;
        }

        void OnDestroy()
        {
            PauseGame.Paused -= OnPause;
            PauseGame.Resumed -= OnResume;
        }

        void OnPause() => _pauseMenuCanvas.enabled = true;

        void OnResume()
        {
            _pauseMenuCanvas.enabled = false;
            _saveGameMenu.Hide();
            _loadGameMenu.Hide();
        }

        void OnContinueButtonSelected()
        {
            PauseGame.Resume();
        }

        void OnSaveGameButtonSelected()
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

        void OnLoadGameButtonSelected()
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

            PauseGame.Resume();
            GameManager.LoadSave(save);
        }

        void OnBackFromLoadGameMenu()
        {
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Hide();
            _pauseMenuCanvas.enabled = true;
        }

        void OnMainMenu()
        {
            PauseGame.Resume();
            GameManager.MainMenu();
        }

        void OnQuitButtonSelected()
        {
            GameManager.Quit();
        }
    }
}