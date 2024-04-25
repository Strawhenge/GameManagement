using FunctionalUtilities;
using System.Collections.Generic;

namespace Strawhenge.GameManagement.Unity
{
    public interface ISaveMetaDataRepository
    {
        IReadOnlyList<SaveMetaData> GetAll();

        Maybe<SaveMetaData> GetMostRecent();
    }
}