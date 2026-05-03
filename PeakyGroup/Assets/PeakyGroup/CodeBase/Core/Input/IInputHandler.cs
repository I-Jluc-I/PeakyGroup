using System;

namespace SelectionSystem.Core.Input
{
    public interface IInputHandler
    {
        event Action OnNext;
        event Action OnPrevious;
        event Action OnConfirm;

        void Enable();
        void Disable();
    }
}