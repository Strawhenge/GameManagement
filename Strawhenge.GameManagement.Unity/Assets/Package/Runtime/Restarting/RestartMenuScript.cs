using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class RestartMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;
        [SerializeField] Canvas _restartMenuCanvas;
        [SerializeField] Button _restartButton;
        [SerializeField] Button _loadGameButton;
        [SerializeField] Button _mainMenuButton;
        [SerializeField] Button _quitButton;

        public IGameManager GameManager { private get; set; }

        public ISaveMetaDataRepository SaveMetaDataRepository { private get; set; }

        public IPlayerState Player { private get; set; }

        void Awake()
        {
            _loadGameMenu.Hide();

            _restartButton.onClick.AddListener(OnRestartButtonSelected);
            _loadGameButton.onClick.AddListener(OnLoadGameButtonSelected);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonSelected);
            _quitButton.onClick.AddListener(OnQuitButtonSelected);
        }

        void Start()
        {
            _restartMenuCanvas.enabled = false;
            Player.Died += OnPlayerDied;
        }

        void OnDestroy()
        {
            Player.Died -= OnPlayerDied;
        }

        void OnPlayerDied() => _restartMenuCanvas.enabled = true;

        void OnRestartButtonSelected()
        {
            var mostRecentSave = SaveMetaDataRepository.GetMostRecent();

            if (mostRecentSave.HasSome(out var save))
                GameManager.LoadSave(save);
            else
                GameManager.StartNewGame();
        }

        void OnLoadGameButtonSelected()
        {
            _restartMenuCanvas.enabled = false;
            _loadGameMenu.Show();
            _loadGameMenu.Load += OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back += OnBackFromLoadGameMenu;
        }

        void OnSaveSelectedFromLoadGameMenu(SaveMetaData save)
        {
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;

            GameManager.LoadSave(save);
        }

        void OnBackFromLoadGameMenu()
        {
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Hide();
            _restartMenuCanvas.enabled = true;
        }

        void OnMainMenuButtonSelected()
        {
            GameManager.MainMenu();
        }

        void OnQuitButtonSelected()
        {
            GameManager.Quit();
        }
    }
}