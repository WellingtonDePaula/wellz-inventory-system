using System;

namespace Wellz.Inventory.Core.Interfaces {
    public interface IInteractable {
        void HoverEnter();
        void HoverExit();
        void Select();
        void Deselect();
    }
}
