using System;
using System.Collections;
using System.Collections.Generic;
using SelectionSystem.Core.Data;
using UnityEngine;

namespace SelectionSystem.Core.UI
{
    public abstract class BaseSelectionView<T> : MonoBehaviour where T : class, ISelectable
    {
        private SelectionContainer<T> _container;
        
        protected void OnDestroy()
        {
            if(_container != null)
                _container.OnSelectionChanged -= OnSelectionChanged;
        }
        
        private void Bind(SelectionContainer<T> container)
        {
            if(_container != null)
                _container.OnSelectionChanged -= OnSelectionChanged;
            
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _container.OnSelectionChanged += OnSelectionChanged;
            
            if(_container.Current != null)
                OnSelectionChanged(_container.Current);
        }
        
        protected abstract void OnSelectionChanged(T data);

        public static IEnumerator LoadAndShow<TView>(string resourcePath, SelectionContainer<T> container, Action<TView> onLoaded, Transform parent) where TView : BaseSelectionView<T>
        {
            var request = Resources.LoadAsync<GameObject>(resourcePath);
            yield return request;

            if (request.asset == null)
            {
                Debug.LogError($"[BaseSelectionView] Resource not found: {resourcePath}");
                yield break;
            }

            var instance = Instantiate((GameObject)request.asset, parent);
            var view = instance.GetComponent<TView>();

            if (view == null)
            {
                Debug.LogError($"[BaseSelectionView] Component {typeof(TView).Name} not found.");
                Destroy(instance);
                yield break;
            }

            view.Bind(container);
            onLoaded?.Invoke(view);
        }

        protected void DestroyView()
        {
            Destroy(gameObject);
        }
    }
}