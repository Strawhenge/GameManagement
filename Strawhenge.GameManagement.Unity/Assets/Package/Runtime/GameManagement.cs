using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;

namespace Strawhenge.GameManagement.Unity
{
    public static class GameManagement // TODO Rename so doesn't share with namespace
    {
        public static IGameManager GameManager { get; internal set; } = NullGameManager.Instance;

        public static SceneNames SceneNames { get; internal set; } = SceneNames.Empty;

        public static ISaveMetaDataRepository SaveMetaDataRepository { get; internal set; }
            = NullSaveMetaDataRepository.Instance;

        public static IPauseGame PauseGame { get; internal set; } = NullPauseGame.Instance;

        public static IRestartGame RestartGame { get; internal set; } = NullRestartGame.Instance;

        public static ISaveGame SaveGame { get; internal set; } = NullSaveGame.Instance;

        internal static ISelectedSaveDataLoader SelectedSaveDataLoader { get; set; }
            = NullSelectedSaveDataController.Instance;

        internal static ISelectedSaveDataState SelectedSaveDataState { get; set; }
            = NullSelectedSaveDataController.Instance;
    }

    public static class GameManagement<TSaveData>
    {
        public static ICurrentSaveDataAccessor<TSaveData> CurrentSaveDataAccessor { get; internal set; }
            = NullCurrentSaveDataContainer<TSaveData>.Instance;
    }
}