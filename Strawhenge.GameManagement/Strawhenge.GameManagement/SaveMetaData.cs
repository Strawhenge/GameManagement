using System;

namespace Strawhenge.GameManagement.Unity
{
    public class SaveMetaData
    {
        public SaveMetaData(Guid id, DateTime dateTimeCreated)
        {
            Id = id;
            DateTimeCreated = dateTimeCreated;
        }

        public Guid Id { get; }

        public DateTime DateTimeCreated { get; }
    }
}