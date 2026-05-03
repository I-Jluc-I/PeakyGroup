using SelectionSystem.Core.Data;
using UnityEngine;

namespace SelectionSystem.Location
{
    [CreateAssetMenu(fileName = "LocationData", menuName = "SelectionSystem/LocationData")]
    public sealed class LocationData : BaseSelectableData
    {
        [SerializeField] private string description;
        public string Description => description;
        
        [SerializeField] private Sprite previewImage;
        public Sprite PreviewImage => previewImage;
        
        [SerializeField] private string sceneId;
        public string SceneId => sceneId;
    }
}