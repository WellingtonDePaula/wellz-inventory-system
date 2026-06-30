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
            if(item != null) {
                if (!item.IsPermanentSlot && quantity == 0) {
                    Debug.LogWarning("ItemData passed does not support permanent slot! Ignoring data.");
                    return;
                }
            }

            this.item = item;
            if (item != null) {
                this.quantity = (item.IsStackable) ? quantity : 1;
            } else {
                this.quantity = 0;
            }

            OnQuantityChanged += ValidateQuantity;

            OnQuantityChanged?.Invoke(this.quantity);
        }

        #region Métodos públicos e privados da lógica da classe
        public bool SetItem(ItemData item) {
            if (this.item != null) { return false; }

            this.item = item;
            this.quantity = 1;

            return true;
        }

        public int AddItem(ItemData item, int addQuantity = 1) {
            if (this.item == null || this.item != item) {
                return addQuantity;
            }

            if (item.IsStackable) {
                int remainingSpace = this.item.MaxStackSize - this.quantity;

                if (addQuantity <= remainingSpace) {
                    this.quantity += addQuantity;

                    OnQuantityChanged?.Invoke(this.quantity);

                    return 0;
                } else {
                    this.quantity = this.item.MaxStackSize;
                    int leftover = addQuantity - remainingSpace;

                    OnQuantityChanged?.Invoke(this.quantity);

                    return leftover;
                }
            }
            return addQuantity;
        }

        public int RemoveItem(ItemData item, int removeQuantity = 1) {
            if (this.item == null || this.quantity <= 0 || this.item != item || removeQuantity <= 0) {
                return 0;
            }

            int difference = this.quantity - removeQuantity;
            bool isStackable = this.item.IsStackable;

            if (difference > 0) {
                this.quantity = difference;
                OnQuantityChanged?.Invoke(this.quantity);
                return removeQuantity;
            } else {
                int removedAmount = this.quantity;

                if (!this.item.IsPermanentSlot) {
                    this.item = null;
                }

                this.quantity = 0;
                OnQuantityChanged?.Invoke(this.quantity);

                if (!isStackable) { return 1; }

                return removedAmount;
            }
        }

        private void ValidateQuantity(int quantity) {
            if(item != null) {
                if (quantity <= 0) {
                    quantity = 0;
                    if (item.IsPermanentSlot) {
                        return;
                    }
                    item = null;
                }
                return;
            }
            quantity = 0;
        }
        #endregion
    }
}