using UnityEngine;

namespace SelectionSystem.Character
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "SelectionSystem/CharacterDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        [SerializeField] private CharacterData[] characters;
        public CharacterData[] Characters => characters;
    }
}