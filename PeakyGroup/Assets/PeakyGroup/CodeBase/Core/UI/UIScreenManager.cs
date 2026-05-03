using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelectionSystem.Core.UI
{
    public class UIScreenManager : MonoBehaviour
    {
        private readonly Dictionary<string, IScreen> _cache  = new Dictionary<string, IScreen>();
        private IScreen _currentScreen;
        
        protected virtual void OnScreenLoaded(IScreen screen) { }

        public void Open(string screenId, string resourcePath)
        {
            if (_currentScreen != null && _currentScreen.ScreenId == screenId)
                return;
            
            if (_cache.TryGetValue(screenId, out IScreen cached))
            {
                SwitchTo(cached);
                return;
            }
            
            StartCoroutine(LoadAndOpen(screenId, resourcePath));
        }
        
        public void CloseCurrentScreen()
        {
            if (_currentScreen == null)
                return;

            _currentScreen.Hide();
            _currentScreen = null;
        }
        
        private IEnumerator LoadAndOpen(string screenId, string resourcePath)
        {
            var request = Resources.LoadAsync<GameObject>(resourcePath);
            yield return request;

            if (request.asset == null)
            {
                Debug.LogError($"[UIScreenManager] Resource not found: {resourcePath}");
                yield break;
            }

            var instance = Instantiate((GameObject)request.asset, transform);
            var screen = instance.GetComponent<IScreen>();

            if (screen == null)
            {
                Debug.LogError($"[UIScreenManager] IScreen not found on: {resourcePath}");
                Destroy(instance);
                yield break;
            }

            _cache[screenId] = screen;
            OnScreenLoaded(screen);
            SwitchTo(screen);
        }
        
        private void SwitchTo(IScreen screen)
        {
            _currentScreen?.Hide();
            _currentScreen = screen;
            _currentScreen.Show();
        }
    }
}