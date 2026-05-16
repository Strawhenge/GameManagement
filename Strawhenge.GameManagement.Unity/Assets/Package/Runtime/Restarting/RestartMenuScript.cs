using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public sealed class RestartMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;
        [SerializeField] Canvas _restartMenuCanvas;
        [SerializeField] Button _restartButton;
        [SerializeField] Button _loadGameButton;
        [SerializeField] Button _mainMenuButton;
        [SerializeField] Button _quitButton;

        void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonSelected);
            _loadGameButton.onClick.AddListener(OnLoadGameButtonSelected);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonSelected);
            _quitButton.onClick.AddListener(OnQuitButtonSelected);
        }

        void Start()
        {
            _restartMenuCanvas.enabled = false;
            GameManager.RestartGame.Restarting += OnRestarting;
        }

        void OnDestroy()
        {
            GameManager.RestartGame.Restarting -= OnRestarting;
        }

        void OnRestarting() => _restartMenuCanvas.enabled = true;

        void OnRestartButtonSelected()
        {
            var mostRecentSave = GameManager.SaveMetaDataRepository.GetMostRecent();

            if (mostRecentSave.HasSome(out var save))
                GameManager.Flow.LoadSave(save);
            else
                GameManager.Flow.StartNewGame();
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

            GameManager.Flow.LoadSave(save);
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
            GameManager.Flow.MainMenu();
        }

        void OnQuitButtonSelected()
        {
            GameManager.Flow.Quit();
        }
    }
}