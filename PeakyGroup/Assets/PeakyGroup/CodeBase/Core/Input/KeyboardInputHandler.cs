using System;
using UnityEngine;

namespace SelectionSystem.Core.Input
{
    public sealed class KeyboardInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] private KeyCode nextKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode previousKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode confirmKey = KeyCode.Return;

        private bool _isEnabled;
        
        public event Action OnNext;
        public event Action OnPrevious;
        public event Action OnConfirm;
        
        public void Enable() => _isEnabled = true;
        public void Disable() => _isEnabled = false;

        private void Update()
        {
            if(!_isEnabled) 
                return;
            
            if(UnityEngine.Input.GetKeyDown(nextKey))
                OnNext?.Invoke();
            if(UnityEngine.Input.GetKeyDown(previousKey))
                OnPrevious?.Invoke();
            if(UnityEngine.Input.GetKeyDown(confirmKey))
                OnConfirm?.Invoke();
        }
    }
}

