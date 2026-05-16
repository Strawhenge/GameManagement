using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.SaveRepository;
using Strawhenge.GameManagement.Saving.Generator;
using Strawhenge.GameManagement.Unity.Flow;
using Strawhenge.GameManagement.Unity.Pausing;
using Strawhenge.GameManagement.Unity.Restarting;
using Strawhenge.GameManagement.Unity.Saving;
using Strawhenge.GameManagement.Unity.Setup;

namespace Strawhenge.GameManagement.Unity
{
    public static class GameManager
    {
        public static IFlowManager Flow { get; internal set; } = NullFlowManager.Instance;

        public static SceneNames SceneNames { get; internal set; } = SceneNames.Empty;

        public static ISaveMetaDataRepository SaveMetaDataRepository { get; internal set; }
            = NullSaveMetaDataRepository.Instance;

        public static IPauseGame PauseGame { get; internal set; } = NullPauseGame.Instance;

        public static IRestartGame RestartGame { get; internal set; } = NullRestartGame.Instance;

        public static ISaveGame SaveGame { get; internal set; } = NullSaveGame.Instance;

        internal static ISelectedSaveDataController SelectedSaveDataController { get; set; }
            = NullSelectedSaveDataController.Instance;

        internal static IClearSaveDataGeneratorSteps ClearSaveSaveGeneratorSteps { get; set; }
            = NullClearSaveDataGeneratorSteps.Instance;
    }

    public static class GameManager<TSaveData>
    {
        internal static ICurrentSaveDataAccessor<TSaveData> CurrentSaveDataAccessor { get; set; }
            = NullCurrentSaveDataContainer<TSaveData>.Instance;

        internal static IAddSaveDataGeneratorStep<TSaveData> AddSaveDataGeneratorSteps { get; set; }
            = NullAddSaveDataGeneratorStep<TSaveData>.Instance;
    }
}