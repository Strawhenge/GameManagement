using FunctionalUtilities;
using Strawhenge.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class InMemorySaveDataRepository : ISaveRepository<SaveData>
{
    readonly Dictionary<Guid, SaveData> _saveDataById = new();
    readonly Dictionary<Guid, SaveMetaData> _saveMetaDataById = new();

    public void Add(SaveData saveData, Guid id, DateTime dateTimeCreated)
    {
        _saveDataById.Add(id, saveData);
        _saveMetaDataById.Add(id, new SaveMetaData(id, dateTimeCreated));
    }

    Task<SaveData> ISaveDataRepository<SaveData>.GetAsync(Guid id)
    {
        return Task.FromResult(_saveDataById[id]);
    }

    Task ISaveDataRepository<SaveData>.DeleteAsync(Guid id)
    {
        _saveDataById.Remove(id);
        _saveMetaDataById.Remove(id);
        return Task.CompletedTask;
    }

    Task ISaveDataRepository<SaveData>.SaveAsync(SaveData saveData)
    {
        var id = Guid.NewGuid();
        _saveDataById.Add(id, saveData);
        _saveMetaDataById.Add(id, new SaveMetaData(id, DateTime.UtcNow));
        return Task.CompletedTask;
    }

    IReadOnlyList<SaveMetaData> ISaveMetaDataRepository.GetAll()
    {
        return _saveMetaDataById.Values.ToArray();
    }

    Maybe<SaveMetaData> ISaveMetaDataRepository.GetMostRecent()
    {
        return _saveMetaDataById.Values
            .OrderByDescending(m => m.DateTimeCreated)
            .FirstOrNone();
    }
}