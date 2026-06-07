using UnityEngine;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Core.Models;
using Wellz.Inventory.Core.Views;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Controllers {
    public class SlotController : MonoBehaviour, ISlotController {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo
        public Vector2Int GridPos { get => gridPos; set => gridPos = value; }

        // Campos privados para o estado interno da classe
        private Vector2Int gridPos;

        private SlotModel model;
        private ISlotView view;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public int AddItem(ItemData item, int quantity = 1) {
            return model.AddItem(item, quantity);
        }

        public int RemoveItem(ItemData item, int quantity = 1) {
            return model.RemoveItem(item, quantity);
        }

        public bool SwapItem(ItemData item) {
            return model.SwapItem(item);
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
        }
        #endregion


    }
}