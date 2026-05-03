using System.Collections;
using SelectionSystem.Core.Input;
using SelectionSystem.Core.UI;
using SelectionSystem.Location;
using SelectionSystem.Location.UI;
using UnityEngine;

namespace SelectionSystem.Bootstrap
{
    public sealed class LocationSceneBootstrap : MonoBehaviour
    {
        private const string Path = "UI/LocationSelectionView";
        
        [SerializeField] private LocationSelector selector;
        [SerializeField] private KeyboardInputHandler keyboardInput;
        [SerializeField] private PointerInputHandler pointerInput;
        [SerializeField] private Transform parent;
        
        private LocationSelectionView _view;
        
        private IEnumerator OpenLocationSelectionView()
        {
            selector.RegisterInputHandler(keyboardInput);
            selector.RegisterInputHandler(pointerInput);
            
            keyboardInput.Enable();
            pointerInput.Enable();

            yield return BaseSelectionView<LocationData>.LoadAndShow<LocationSelectionView>(Path, selector.Container,
                view =>
                {
                    _view = view;
                    _view.SetSelector(selector);
                },
                parent);
        }
        
        private void OnDestroy()
        {
            if (selector == null)
                return;
            
            selector.UnregisterInputHandler(keyboardInput);
            selector.UnregisterInputHandler(pointerInput);
        }

        public void StartOpenLocationSelectionView()
        {
            StartCoroutine(OpenLocationSelectionView());
        }
    }
}