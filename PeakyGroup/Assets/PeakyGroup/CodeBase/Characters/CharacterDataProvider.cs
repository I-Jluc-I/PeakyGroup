using System;
using System.Collections.Generic;
using SelectionSystem.Core.Data;
using UnityEngine;

namespace SelectionSystem.Character
{
    public sealed class CharacterDataProvider : IDataProvider<CharacterData>
    {
        private readonly CharacterDatabase _database;
        private readonly Dictionary<string, CharacterData> _cache;

        public CharacterDataProvider(CharacterDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _cache = new Dictionary<string, CharacterData>();
        }

        public CharacterData GetData(string id)
        {
            if(_cache.TryGetValue(id, out var cached))
                return cached;

            foreach (var character in _database.Characters)
            {
                if(character.Id != id)
                    continue;
                
                _cache[id] = character;
                return character;
            }
            
            Debug.LogError($"[CharacterDataProvider] Character not found: {id}");
            return null;
        }
        
        public string[] GetAllIds()
        {
            var ids = new string[_database.Characters.Length];

            for (var i = 0; i < _database.Characters.Length; i++)
            {
                ids[i] = _database.Characters[i].Id;
            }
            return ids;
        }
    }
}