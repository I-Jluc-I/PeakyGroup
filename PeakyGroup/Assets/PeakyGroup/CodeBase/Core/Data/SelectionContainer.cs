using System;

namespace SelectionSystem.Core.Data
{
    public class SelectionContainer <T> where T : ISelectable
    {
        public T Current { get; private set; }
        
        public event Action<T> OnSelectionChanged;

        public void Select(T data)
        {
            if(data == null) 
                return;

            Current = data;
            OnSelectionChanged?.Invoke(Current);
        }

        public void Clear()
        {
            Current = default;
            OnSelectionChanged?.Invoke(Current);
        }
    }
}