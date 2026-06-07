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

        public int Quantity => quantity;

        // Construtores
        public SlotModel(ItemData item, int quantity) {
            this.item = item;
            this.quantity = quantity;
        }
        public SlotModel(ItemData item) {
            this.item = item;
            this.quantity = 0;
        }

        #region Métodos públicos e privados da lógica da classe
        #endregion

        //public bool SwapSlot(SlotModel slot) {
        //    if (slot == null) {
        //        return false;
        //    }
        //    if (ReferenceEquals(this, slot)) {
        //        return false;
        //    }

        //    Vector2Int tempPos = this.gridPos;

        //    this.gridPos = slot.gridPos;

        //    slot.gridPos = tempPos;

        //    return true;
        //}

        public bool SwapItem(ItemData item) {
            if (this.item != null) { return false; }

            this.item = item;
            this.quantity = 1;

            return true;
        }

        public int AddItem(ItemData item, int addQuantity = 1) {
            if (this.item == null || this.quantity <= 0 || this.item != item) {
                return addQuantity;
            }

            if (item.IsStackable) {
                int remainingSpace = this.item.MaxStackSize - this.quantity;

                if (addQuantity <= remainingSpace) {
                    this.quantity += addQuantity;
                    return 0;
                } else {
                    this.quantity = this.item.MaxStackSize;
                    int leftover = addQuantity - remainingSpace;
                    return leftover;
                }
            }
            return addQuantity;
        }

        public int RemoveItem(ItemData item, int removeQuantity = 1) {
            if (this.item == null || this.quantity <= 0 || this.item != item || removeQuantity <= 0) {
                return 0;
            }

            if (!this.item.IsStackable) {
                this.item = null;
                this.quantity = 0;
                return 1;
            }

            int difference = this.quantity - removeQuantity;

            if (difference > 0) {
                this.quantity = difference;
                return removeQuantity;
            } else {
                int removedAmount = this.quantity;

                this.item = null;
                this.quantity = 0;

                return removedAmount;
            }
        }
    }
}