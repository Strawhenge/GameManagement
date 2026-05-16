using System;
using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.GameManagement.SaveRepository
{
    public class NullSaveMetaDataRepository : ISaveMetaDataRepository
    {
        public static ISaveMetaDataRepository Instance { get; } = new NullSaveMetaDataRepository();

        NullSaveMetaDataRepository()
        {
        }

        public IReadOnlyList<SaveMetaData> GetAll() => Array.Empty<SaveMetaData>();

        public Maybe<SaveMetaData> GetMostRecent() => Maybe.None<SaveMetaData>();
    }
}