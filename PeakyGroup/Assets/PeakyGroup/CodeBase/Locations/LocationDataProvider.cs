using System;
using System.Collections.Generic;
using SelectionSystem.Core.Data;
using UnityEngine;

namespace SelectionSystem.Location
{
    public sealed class LocationDataProvider : IDataProvider<LocationData>
    {
        private readonly LocationDatabase _database;
        private readonly Dictionary<string, LocationData> _cache;

        public LocationDataProvider(LocationDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _cache = new Dictionary<string, LocationData>();
        }

        public LocationData GetData(string id)
        {
            if (_cache.TryGetValue(id, out var cached))
                return cached;

            foreach (var location in _database.Locations)
            {
                if (location.Id != id)
                    continue;
                
                _cache[id] = location;
                return location;
            }
            
            Debug.LogError($"[LocationDataProvider] Location not found: {id}");
            return null;
        }

        public string[] GetAllIds()
        {
            var ids = new string[_database.Locations.Length];
            
            for (var i = 0; i < ids.Length; i++)
                ids[i] = _database.Locations[i].Id;
            
            return ids;
        }
    }
}