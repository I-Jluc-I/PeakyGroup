using UnityEngine;

namespace SelectionSystem.Location
{
    [CreateAssetMenu(fileName = "LocationDatabase", menuName = "SelectionSystem/LocationDatabase")]
    public sealed class LocationDatabase : ScriptableObject
    {
        [SerializeField] private LocationData[] locations;
        public LocationData[] Locations => locations;
    }
}