using UnityEngine;
using Wellz.Inventory.Input;
using Wellz.Inventory.Items;
using Wellz.Utils.Core;

namespace Wellz.Inventory.Core.Controllers {
    public class InventoryController : InventoryControllerBase {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        protected override void HandleOnPressed() {
            if (currentHoverSlot == null) {
                return;
            }
            if (!IsSlotAvailableToSelect(currentHoverSlot)) {
                return;
            }

            bool clickedSameSlot = (currentSelectedSlot == currentHoverSlot);

            currentSelectedSlot?.SelectSlot(false);
            currentSelectedSlot = null;

            if (!clickedSameSlot) {
                currentHoverSlot.SelectSlot(true);
                currentSelectedSlot = currentHoverSlot;
            }
        }
        protected override void HandleOnReleased() {
            //throw new NotImplementedException();
        }
        protected override void HandleOnOtherReleased() {
            //throw new NotImplementedException();
        }

        protected override void HandleOnOtherPressed() {
            if (currentSelectedSlot == null) { return; }
            //currentSelectedSlot.AddItem(currentSelectedSlot.Item, 1);
            currentSelectedSlot.RemoveItem(currentSelectedSlot.Item, 1);
        }
        protected override void HandleOnPositionChanged(Vector2 pos) {
            SlotControllerBase slotUnder = FindSlotUnderPoint(pos);

            if (slotUnder != currentHoverSlot) {
                if (currentHoverSlot != null) {
                    currentHoverSlot.FocusSlot(false);
                }

                currentHoverSlot = slotUnder;

                if (currentHoverSlot != null) {
                    currentHoverSlot.FocusSlot(true);
                }
            }
        }
        protected override void HandleOnItemEnded(SlotControllerBase slotController) {
            if (currentSelectedSlot == slotController) {
                currentSelectedSlot?.SelectSlot(false);
                currentSelectedSlot = null;
            }
        }

        protected override bool IsSlotAvailableToSelect(SlotControllerBase slot) {
            return slot != null && !slot.IsEmpty;
        }

        private SlotControllerBase FindSlotUnderPoint(Vector2 screenPoint) {
            for (int x = 0; x < inventoryGrid.Width; x++) {
                for (int y = 0; y < inventoryGrid.Height; y++) {
                    var slot = inventoryGrid.GetValue(x, y);
                    if (slot.ContainsScreenPoint(screenPoint, eventCamera)) {
                        return slot;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}