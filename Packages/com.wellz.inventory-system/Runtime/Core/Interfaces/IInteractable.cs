using System;

namespace Wellz.Inventory.Core.Interfaces {
    public interface IInteractable {
        void HoverEnter(IInputProvider input);
        void HoverExit();
        void Select();
        void Deselect();
    }
}
