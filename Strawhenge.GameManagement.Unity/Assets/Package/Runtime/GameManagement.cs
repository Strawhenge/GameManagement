using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;

namespace Strawhenge.GameManagement.Unity
{
    public static class GameManagement
    {
        public static IGameManager GameManager { get; internal set; } = NullGameManager.Instance;

        public static SceneNames SceneNames { get; internal set; }

        public static ISaveMetaDataRepository SaveMetaDataRepository { get; internal set; }

        public static IPauseGame PauseGame { get; internal set; } = NullPauseGame.Instance;

        public static IRestartGame RestartGame { get; internal set; } = NullRestartGame.Instance;

        public static ISaveGame SaveGame { get; internal set; } = NullSaveGame.Instance;

        public static ISelectedSaveDataLoader SelectedSaveDataLoader { get; internal set; }

        public static ISelectedSaveDataState SelectedSaveDataState { get; internal set; }
    }

    public static class GameManagement<TSaveData>
    {
        public static ICurrentSaveDataAccessor<TSaveData> CurrentSaveDataAccessor { get; internal set; }
    }
}