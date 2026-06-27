using UnityEngine;
using UnityEngine.UI;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Interfaces {
    public interface ISlotView : IInteractable {
        public void SetupView(ItemData data, int quantity);
        public void RefreshView(ItemData data, int quantity);
        public void SwapItem(ItemData data, int quantity);
        public void Clear();
    }
}