using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class PauseMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;
        [SerializeField] SaveGameMenuScript _saveGameMenu;
        [SerializeField] GameObject _pauseMenuCanvas;
        [SerializeField] GameObject _pauseMenuContent;
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

            _continueButton.onClick.AddListener(OnContinue);
            _saveGameButton.onClick.AddListener(OnSaveGameButtonSelected);
            _loadGameButton.onClick.AddListener(OnLoadGameButtonSelected);
            _mainMenuButton.onClick.AddListener(OnMainMenu);
            _quitButton.onClick.AddListener(OnQuit);
        }

        void Start()
        {
            _pauseMenuCanvas.SetActive(false);
            PauseGame.Paused += OnPause;
            PauseGame.Resumed += OnResume;
        }

        void OnDestroy()
        {
            PauseGame.Paused -= OnPause;
            PauseGame.Resumed -= OnResume;
        }

        void OnPause() => _pauseMenuCanvas.SetActive(true);

        void OnResume() => _pauseMenuCanvas.SetActive(false);

        void OnContinue()
        {
            PauseGame.Resume();
        }

        void OnSaveGameButtonSelected()
        {
            _pauseMenuContent.SetActive(false);
            _saveGameMenu.Show();
            _saveGameMenu.Back += OnReturnFromSaveGameMenu;
            _saveGameMenu.Saved += OnReturnFromSaveGameMenu;
        }

        void OnReturnFromSaveGameMenu()
        {
            _saveGameMenu.Back -= OnReturnFromSaveGameMenu;
            _saveGameMenu.Saved -= OnReturnFromSaveGameMenu;
            _saveGameMenu.Hide();
            _pauseMenuContent.SetActive(true);
        }

        void OnLoadGameButtonSelected()
        {
            _pauseMenuContent.SetActive(false);
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
            _pauseMenuContent.SetActive(true);
        }

        void OnMainMenu()
        {
            PauseGame.Resume();
            GameManager.MainMenu();
        }

        void OnQuit()
        {
            GameManager.Quit();
        }
    }
}