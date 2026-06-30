using System;
using UnityEngine;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Models {
    public class SlotModel {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private ItemData item;
        private int quantity;

        // Propriedades para acesso controlado externo
        public ItemData Item => item;
        public event Action<int> OnQuantityChanged;
        public bool IsEmpty => (item == null);

        public int Quantity => quantity;

        // Construtor
        public SlotModel(ItemData item, int quantity) {
            if (item != null && !item.IsPermanentSlot && quantity <= 0) {
                Debug.LogWarning("ItemData passed does not support permanent slot! Ignoring data.");
                item = null;
            }

            this.item = item;
            this.quantity = (this.item != null) ? (this.item.IsStackable ? quantity : 1) : 0;

            OnQuantityChanged += ValidateQuantity;
            OnQuantityChanged?.Invoke(this.quantity);
        }

        #region Métodos públicos e privados da lógica da classe
        public bool SetItem(ItemData item) {
            if (this.item != null) { return false; }

            this.item = item;
            this.quantity = 1;

            OnQuantityChanged?.Invoke(this.quantity);

            return true;
        }

        public int AddItem(ItemData item, int addQuantity = 1) {
            if (this.item == null || this.item != item || !item.IsStackable) {
                return addQuantity;
            }

            int remainingSpace = this.item.MaxStackSize - this.quantity;
            int amountToAdd = Mathf.Min(addQuantity, remainingSpace);
            int leftover = addQuantity - amountToAdd;

            if (amountToAdd > 0) {
                this.quantity += amountToAdd;
                OnQuantityChanged?.Invoke(this.quantity);
            }

            return leftover;
        }

        public int RemoveItem(ItemData item, int removeQuantity = 1) {
            if (this.item == null || this.item != item || removeQuantity <= 0) {
                return removeQuantity;
            }

            int amountToRemove = Mathf.Min(removeQuantity, this.quantity);
            int leftover = removeQuantity - amountToRemove;

            this.quantity -= amountToRemove;
            OnQuantityChanged?.Invoke(this.quantity);

            return leftover;
        }

        private void ValidateQuantity(int newQuantity) {
            if (item == null) {
                quantity = 0;
                return;
            }

            if (newQuantity > 0) {
                return;
            }

            quantity = 0;

            if (!item.IsPermanentSlot) {
                item = null;
            }
        }
        #endregion
    }
}