using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wellz.Inventory.Core.Models;
using Wellz.Inventory.Input;
using Wellz.Utils.Core;

namespace Wellz.Inventory.Core.Controllers {
    public abstract class InventoryControllerBase : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected int width;
        [SerializeField] protected int height;
        [SerializeField] protected Transform slotsTransform;
        [SerializeField] protected Canvas rootCanvas;

        [SerializeField] protected InputProvider inputProvider;

        [SerializeField] protected List<InventoryStartItem> initialItems;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        protected SlotControllerBase currentHoverSlot = null;
        protected SlotControllerBase currentSelectedSlot = null;
        protected GenericGrid<SlotControllerBase> inventoryGrid;
        protected Camera eventCamera;

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        protected virtual void Awake() {
            eventCamera = UtilsClass.ResolveEventCamera(rootCanvas);

            inventoryGrid = new GenericGrid<SlotControllerBase>(width, height, 1, default, (grid, x, y) => {
                var slot = Instantiate(slotPrefab, slotsTransform).GetComponent<SlotControllerBase>();
                slot.CreateSlot(new Vector2Int(x, y));
                return slot;
            });
            InitializeItems();
        }

        protected virtual void OnEnable() {
            if (inputProvider != null) {
                inputProvider.Activate();
                inputProvider.OnPressed += HandleOnPressed;
                inputProvider.OnReleased += HandleOnReleased;

                inputProvider.OnOtherPressed += HandleOnOtherPressed;
                inputProvider.OnOtherReleased += HandleOnOtherReleased;

                inputProvider.OnPositionChanged += HandleOnPositionChanged;
            }
            if (inventoryGrid != null) {
                inventoryGrid.ForEach((x, y, slot) => {
                    slot.OnItemEnded += HandleOnItemEnded;
                });
            }
        }

        protected virtual void OnDisable() {
            if (inputProvider != null) {
                inputProvider.OnPressed -= HandleOnPressed;
                inputProvider.OnReleased -= HandleOnReleased;

                inputProvider.OnOtherPressed -= HandleOnOtherPressed;
                inputProvider.OnOtherReleased -= HandleOnOtherReleased;

                inputProvider.OnPositionChanged -= HandleOnPositionChanged;
                inputProvider.Deactivate();
            }
            if (inventoryGrid != null) {
                inventoryGrid.ForEach((x, y, slot) => {
                    slot.OnItemEnded -= HandleOnItemEnded;
                });
            }
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        protected abstract void HandleOnPressed();
        protected abstract void HandleOnReleased();

        protected abstract void HandleOnOtherReleased();
        protected abstract void HandleOnOtherPressed();

        protected abstract void HandleOnPositionChanged(Vector2 pos);

        protected abstract void HandleOnItemEnded(SlotControllerBase slotController);
        protected abstract bool IsSlotAvailableToSelect(SlotControllerBase slot);
        protected virtual void InitializeItems() {
            List<SlotControllerBase> allSlots = inventoryGrid.GetAllValues().ToList();
            for (int i = 0; i < allSlots.Count; i++) {
                var slot = allSlots[i];

                if (i <= initialItems.Count - 1) {
                    slot.Setup(initialItems[i].Item, initialItems[i].Quantity);
                    continue;
                }

                slot.Setup(null, 0);
            }
        }
        #endregion

    }
}