using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.GameManagement
{
    public interface ISaveMetaDataRepository
    {
        IReadOnlyList<SaveMetaData> GetAll();

        Maybe<SaveMetaData> GetMostRecent();
    }
}