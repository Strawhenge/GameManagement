using FunctionalUtilities;

namespace Strawhenge.GameManagement.Unity.Tests.PostGameSceneLoadedSegments
{
    public class StoreSaveDataSegmentScript : PostGameSceneLoadedSegmentScript<SaveData.SaveData>
    {
        public static Maybe<SaveData.SaveData> SaveData { get; private set; }

        bool _isCompleted;

        public override bool IsCompleted => _isCompleted;

        public override void Run()
        {
            SaveData = CurrentSaveData;
            _isCompleted = true;
        }
    }
}