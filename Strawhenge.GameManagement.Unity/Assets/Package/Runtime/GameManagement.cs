using Strawhenge.GameManagement.CurrentSaveData;
using Strawhenge.GameManagement.Loading;
using Strawhenge.GameManagement.Saving;

namespace Strawhenge.GameManagement.Unity
{
    public static class GameManagement
    {
        public static IGameManager GameManager { get; internal set; }

        public static ISceneNames SceneNames { get; internal set; }

        public static ISaveMetaDataRepository SaveMetaDataRepository { get; internal set; }

        public static PauseGame PauseGame { get; internal set; }

        public static RestartGame RestartGame { get; internal set; }

        public static ISaveGameCommandFactory SaveCommandFactory { get; internal set; }
        
        public static ISelectedSaveDataLoader SelectedSaveDataLoader { get; internal set; }
        
        public static ISelectedSaveDataState SelectedSaveDataState { get; internal set; }
    }

    public static class GameManagement<TSaveData>
    {
        public static ICurrentSaveDataAccessor<TSaveData> CurrentSaveDataAccessor { get; internal set; }
    }
}