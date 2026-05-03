using UnityEngine;

namespace SelectionSystem.Core.Data
{
    public abstract class BaseSelectableData : ScriptableObject, ISelectable
    {
        [SerializeField] private string id;
        public string Id => id;
        
        [SerializeField] private string displayName;
        public string DisplayName => displayName;
    }
}