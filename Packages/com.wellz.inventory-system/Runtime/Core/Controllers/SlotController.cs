using System;
using UnityEngine;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Core.Models;
using Wellz.Inventory.Input;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Controllers {
    [RequireComponent(typeof(ISlotView))]
    public class SlotController : MonoBehaviour, ISlotController {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private RectTransform rectTransform; 

        // Propriedades para acesso controlado externo
        public Vector2Int GridPos { get => gridPos; set => gridPos = value; }
        public RectTransform RectTransform => rectTransform;

        // Campos privados para o estado interno da classe
        private Vector2Int gridPos;

        private SlotModel model;
        private ISlotView view;

        private bool hover = false;
        private bool selected = false;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)

        private void Awake() {
            view = GetComponent<ISlotView>();
        }

        private void OnDestroy() {
            if (model != null)
                model.OnQuantityChanged -= HandleModelChanged;
        }

        #endregion

        #region Métodos públicos e privados da lógica da classe

        public int RemoveItem(ItemData item, int quantity = 1) {
            int remainder = model.RemoveItem(item, quantity);
            return remainder;
        }

        public bool SwapItem(ItemData item) {
            bool swapped = model.SetItem(item);
            view.SwapItem(item, model.Quantity);
            return swapped;
        }

        public bool SwapSlot(ISlotController slot) {
            if (slot == null) {
                return false;
            }
            if (ReferenceEquals(this, slot)) {
                return false;
            }

            Vector2Int tempPos = this.gridPos;

            this.gridPos = slot.GridPos;

            slot.GridPos = tempPos;

            return true;
        }

        public void Setup(Vector2Int gridPos, ItemData item = null, int quantity = 0) {
            this.gridPos = gridPos;
            model = new SlotModel(item, quantity);

            model.OnQuantityChanged += HandleModelChanged;

            view.SetupView(model.Item, model.Quantity);
        }

        public void HoverSlot(bool isHover) {
            hover = isHover;
            if (isHover) {
                view.HoverEnter();
                return;
            }
            view.HoverExit();
        }
        public void SelectSlot() {
            selected = !selected;
            if (selected) {
                view.Select();
                return;
            }
            view.Deselect();
        }

        public int AddItem(ItemData item, int quantity = 1) {
            return model.AddItem(item, quantity);
        }

        private void HandleModelChanged() {
            view.RefreshView(model.Item, model.Quantity);
        }
        #endregion


    }
}