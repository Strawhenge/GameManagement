using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.GameManagement.Unity
{
    public class MainMenuScript : MonoBehaviour
    {
        [SerializeField] LoadGameMenuScript _loadGameMenu;

        [SerializeField] Button _continueButton;
        [SerializeField] Button _newGameButton;
        [SerializeField] Button _loadGameButton;
        [SerializeField] Button _quitButton;

        public IGameManager GameManager { private get; set; }

        public ISaveMetaDataRepository SaveMetaDataRepository { private get; set; }

        void Awake()
        {
            _loadGameMenu.Hide();
            _continueButton.onClick.AddListener(OnContinue);
            _newGameButton.onClick.AddListener(OnNewGame);
            _loadGameButton.onClick.AddListener(OnLoadGame);
            _quitButton.onClick.AddListener(OnQuit);
        }

        void OnContinue()
        {
            var mostRecentSave = SaveMetaDataRepository.GetMostRecent();

            mostRecentSave.Do(
                GameManager.LoadSave);
        }

        void OnNewGame()
        {
            GameManager.StartNewGame();
        }

        void OnLoadGame()
        {
            gameObject.SetActive(false);
            _loadGameMenu.Show();
            _loadGameMenu.Back += OnBackFromLoadGameMenu;
            _loadGameMenu.Load += OnSaveSelectedFromLoadGameMenu;
        }

        void OnBackFromLoadGameMenu()
        {
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            _loadGameMenu.Hide();
            gameObject.SetActive(true);
        }

        void OnSaveSelectedFromLoadGameMenu(SaveMetaData save)
        {
            _loadGameMenu.Back -= OnBackFromLoadGameMenu;
            _loadGameMenu.Load -= OnSaveSelectedFromLoadGameMenu;
            
            GameManager.LoadSave(save);
        }

        void OnQuit()
        {
            GameManager.Quit();
        }
    }
}