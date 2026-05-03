using System.Collections;
using SelectionSystem.Character;
using SelectionSystem.Core.Input;
using SelectionSystem.Core.UI;
using UnityEngine;

namespace SelectionSystem.Bootstrap
{
    public sealed class CharacterSceneBootstrap : MonoBehaviour
    {
        private const string Path = "UI/CharacterSelectionView";
        
        [SerializeField] private CharacterSelector selector;
        [SerializeField] private KeyboardInputHandler keyboardInput;
        [SerializeField] private PointerInputHandler pointerInput;
        [SerializeField] private Transform parent;

        private CharacterSelectionView _view;
        
        private IEnumerator OpenCharacterSelectionView()
        {
            selector.RegisterInputHandler(keyboardInput);
            selector.RegisterInputHandler(pointerInput);
            
            keyboardInput.Enable();
            pointerInput.Enable();

            yield return BaseSelectionView<CharacterData>.LoadAndShow<CharacterSelectionView>(Path, selector.Container,
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

        public void StartOpenCharacterSelectionView()
        {
            StartCoroutine(OpenCharacterSelectionView());
        }
    }
}