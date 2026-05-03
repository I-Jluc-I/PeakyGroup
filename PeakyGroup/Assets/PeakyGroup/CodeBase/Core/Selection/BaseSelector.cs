using System;
using SelectionSystem.Core.Data;
using SelectionSystem.Core.Input;
using UnityEngine;

namespace SelectionSystem.Core.Selection
{
    public abstract class BaseSelector<T> : MonoBehaviour where T : class, ISelectable
    {
        private IDataProvider<T> _dataProvider;
        private string[] _ids;
        private int _currentIndex;
        
        public SelectionContainer<T> Container { get; private set; }

        protected void Initialize(IDataProvider<T> dataProvider)
        {
            if(dataProvider == null) 
                throw new ArgumentNullException(nameof(dataProvider));
            
            _dataProvider = dataProvider;
            Container = new SelectionContainer<T>();
            _ids = _dataProvider.GetAllIds();

            if(_ids.Length > 0) 
                SelectByIndex(0);
        }

        public void SelectNext()
        {
            if(_ids == null || _ids.Length == 0) 
                return;

            _currentIndex = (_currentIndex + 1) % _ids.Length;
            SelectByIndex(_currentIndex);
        }

        public void SelectPrevious()
        {
            if(_ids == null || _ids.Length == 0) 
                return;
            
            _currentIndex = (_currentIndex - 1 + _ids.Length) % _ids.Length;
            SelectByIndex(_currentIndex);
        }

        public void RegisterInputHandler(IInputHandler inputHandler)
        {
            inputHandler.OnNext += SelectNext;
            inputHandler.OnPrevious += SelectPrevious;
        }

        public void UnregisterInputHandler(IInputHandler inputHandler)
        {
            inputHandler.OnNext -= SelectNext;
            inputHandler.OnPrevious -= SelectPrevious;
        }

        private void SelectByIndex(int index)
        {
            var data = _dataProvider.GetData(_ids[index]);
            Container.Select(data);
        }
    }
}