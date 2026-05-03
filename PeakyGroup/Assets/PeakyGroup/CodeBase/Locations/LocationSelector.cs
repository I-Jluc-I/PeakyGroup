using System.Collections;
using SelectionSystem.Core.Selection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelectionSystem.Location
{
    public class LocationSelector : BaseSelector<LocationData>
    {
        [SerializeField] private LocationDatabase database;

        private void Awake()
        {
            Initialize(new LocationDataProvider(database));
        }

        public void LoadCurrentLocation()
        {
            if (Container.Current == null)
            {
                Debug.LogWarning($"[LocationSelector] No location selected");
                return;
            }
            
            StartCoroutine(LoadSceneCoroutine(Container.Current.SceneId));
        }

        private IEnumerator LoadSceneCoroutine(string sceneId)
        {
            var operation = SceneManager.LoadSceneAsync(sceneId);
            operation!.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                yield return null;
            }
            
            operation.allowSceneActivation = true;
        }
    }
}