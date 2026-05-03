using SelectionSystem.Core.Data;
using UnityEngine;

namespace SelectionSystem.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "SelectionSystem/CharacterData")]
    public sealed class CharacterData : BaseSelectableData
    {
        [SerializeField] private int level;
        public int Level => level;
        
        [SerializeField] private Sprite avatar;
        public Sprite Avatar => avatar;
        
        [SerializeField] private GameObject model3DPrefab;
        public GameObject Model3DPrefab => model3DPrefab;
    }
}