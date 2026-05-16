using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;

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

        internal static IClearSaveSaveGeneratorSteps ClearSaveSaveGeneratorSteps { get; set; } // TODO Default value.
    }

    public static class GameManager<TSaveData>
    {
        internal static ICurrentSaveDataAccessor<TSaveData> CurrentSaveDataAccessor { get; set; }
            = NullCurrentSaveDataContainer<TSaveData>.Instance;

        internal static SaveDataGeneratorSteps<TSaveData>
            SaveDataGeneratorSteps { get; set; } // TODO Default value.
    }
}