using System;
using UnityEngine;

namespace SelectionSystem.Core.Input
{
    public class PointerInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] private float swipeThreshold = 50f;

        private bool _isEnabled;
        private Vector2 _touchStartPosition;
        private bool _isDragging;

        public event Action OnNext;
        public event Action OnPrevious;
        public event Action OnConfirm;
        
        public void Enable() => _isEnabled = true;
        public void Disable() => _isEnabled = false;

        public void Update()
        {
            if(!_isEnabled) return;
            
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            HandleMouseInput();
        #else
            HandleTouchInput();
        #endif
        }

        private void HandleMouseInput()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0))
            {
                _touchStartPosition = UnityEngine.Input.mousePosition;
                _isDragging = true;
            }

            if(UnityEngine.Input.GetMouseButtonUp(0) && _isDragging)
            {
                var delta = (Vector2)UnityEngine.Input.mousePosition - _touchStartPosition;
                ProcessSwipeDelta(delta);
                _isDragging = false;
            }
        }

        private void HandleTouchInput()
        {
            if(UnityEngine.Input.touchCount == 0) 
                return;
            
            var touch = UnityEngine.Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPosition = touch.position;
                    _isDragging = true;
                    break;
                
                case TouchPhase.Ended:
                    if(_isDragging)
                    {
                        var delta = touch.position - _touchStartPosition;
                        ProcessSwipeDelta(delta);
                        _isDragging = false;
                    }
                    break;
                
                case TouchPhase.Canceled:
                    _isDragging = false;
                    break;
            }
        }

        private void ProcessSwipeDelta(Vector2 delta)
        {
            if(Mathf.Abs(delta.x) < swipeThreshold)
            {
                if(Mathf.Abs(delta.y) < 10f)
                    OnConfirm?.Invoke();
                return;
            }
            
            if(delta.x > 0)
                OnNext?.Invoke();
            else
                OnPrevious?.Invoke();
        }
    }
}