using SelectionSystem.Core.Input;
using SelectionSystem.Core.Selection;
using UnityEngine;

namespace SelectionSystem.Character
{
    public sealed class CharacterSelector : BaseSelector<CharacterData>
    {
        [SerializeField] private CharacterDatabase database;

        private void Awake()
        {
            Initialize(new CharacterDataProvider(database));
        }
    }
}