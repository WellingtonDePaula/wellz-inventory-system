using UnityEngine;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Core.Models;
using Wellz.Inventory.Input;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Controllers {
    public class SlotController : MonoBehaviour, ISlotController {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // TEMPORÁRIO ATÉ A CRIAÇÃO DO INVENTORY CONTROLLER
        [SerializeField] private InputProvider inputProvider;
        //-----------------------------------------------//

        // Propriedades para acesso controlado externo
        public Vector2Int GridPos { get => gridPos; set => gridPos = value; }

        // Campos privados para o estado interno da classe
        private Vector2Int gridPos;

        private SlotModel model;
        private ISlotView view;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)

        private void Awake() {
            view = GetComponent<ISlotView>();
        }

        private void Update() {
            view.HoverEnter(inputProvider);
        }

        private void OnDestroy() {
            // Lembre-se de remover a assinatura para evitar memory leaks
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

            // O Controller assina o evento do Model
            model.OnQuantityChanged += HandleModelChanged;

            view.SetupView(model.Item, model.Quantity);
        }

        private void HandleModelChanged() {
            // Sempre que o model mudar por QUALQUER motivo, a view se atualiza
            view.RefreshView(model.Item, model.Quantity);
        }

        public int AddItem(ItemData item, int quantity = 1) {
            // O controller só altera o model. A chamada de RefreshView agora é automática pelo evento.
            return model.AddItem(item, quantity);
        }
        #endregion


    }
}