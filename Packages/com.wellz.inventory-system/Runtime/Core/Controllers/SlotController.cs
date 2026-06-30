using System;
using UnityEngine;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Core.Models;
using Wellz.Inventory.Input;
using Wellz.Inventory.Items;
using Wellz.Utils.Core;

namespace Wellz.Inventory.Core.Controllers {
    public class SlotController : SlotControllerBase {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        #endregion

        #region Métodos públicos e privados da lógica da classe

        public override int RemoveItem(ItemData item, int quantity = 1) {
            if (model == null || item == null || quantity <= 0) {
                return quantity;
            }

            return model.RemoveItem(item, quantity);
        }

        public override int AddItem(ItemData item, int quantity = 1) {
            if (model == null || item == null || quantity <= 0) {
                return quantity;
            }

            return model.AddItem(item, quantity);
        }

        public override bool SwapItem(ItemData item) {
            if (model == null) {
                return false;
            }

            bool swapped = model.SetItem(item);

            return swapped;
        }

        public override bool SwapSlot(ISlotController slot) {
            if (slot == null) {
                return false;
            }
            if (ReferenceEquals(this, slot)) {
                return false;
            }

            // aqui só é trocada a posição na grid entre os dois
            // controllers, não os itens. Se a intenção for trocar o conteúdo
            // (itens/quantidades) dos slots, a lógica precisa mudar.
            Vector2Int tempPos = gridPos;
            gridPos = slot.GridPos;
            slot.GridPos = tempPos;

            return true;
        }

        public override void Setup(ItemData item = null, int quantity = 0) {
            UnsubscribeFromModel();

            model = new SlotModel(item, quantity);
            model.OnQuantityChanged += HandleModelChanged;

            view.SetupView(model.Item, model.Quantity);
        }

        protected override void HandleModelChanged(int quantity) {
            view.RefreshView(model.Item, quantity);

            if (!model.IsEmpty) {
                return;
            }

            if (isSelected) {
                SelectSlot(false);
            }

            InvokeOnItemEnded(this);
        }

        public override void FocusSlot(bool focus) {
            isFocused = focus;
            if (isSelected) { return; }

            if (focus) {
                view.FocusStarted();
                return;
            }
            view.FocusEnded();
        }

        public override void SelectSlot(bool select) {
            isSelected = select;

            if (select) {
                view.Select();
                return;
            }
            view.Deselect();
            if (isFocused) {
                view.FocusStarted();
            }
        }
        #endregion


    }
}