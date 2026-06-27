using UnityEngine;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Interfaces {
    public interface ISlotController {
        Vector2Int GridPos { get; set; }

        bool SwapSlot(ISlotController slot);
        bool SwapItem(ItemData item);
        int AddItem(ItemData item, int quantity = 1);
        int RemoveItem(ItemData item, int quantity = 1);
        void Setup(Vector2Int gridPos, ItemData item = null, int quantity = 0);
    }
}