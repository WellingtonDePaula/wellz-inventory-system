using System;
using UnityEngine;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Core.Models;
using Wellz.Inventory.Core.Views;
using Wellz.Inventory.Input;
using Wellz.Inventory.Items;

namespace Wellz.Inventory.Core.Controllers {
    [RequireComponent(typeof(SlotViewBase))]
    public abstract class SlotControllerBase : MonoBehaviour, ISlotController {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] protected RectTransform rectTransform;

        // Propriedades para acesso controlado externo
        public Vector2Int GridPos { get => gridPos; set => gridPos = value; }
        public RectTransform RectTransform => rectTransform;
        public ItemData Item => model?.Item;
        public bool IsFocused => isFocused;
        public bool IsSelected => isSelected;

        // Campos privados para o estado interno da classe
        protected Vector2Int gridPos;

        protected SlotModel model;
        protected ISlotView view;

        protected bool isFocused = false;
        protected bool isSelected = false;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)

        protected virtual void Awake() {
            view = GetComponent<ISlotView>();
        }

        protected virtual void OnDestroy() {
            UnsubscribeFromModel();
        }

        #endregion

        #region Métodos públicos e privados da lógica da classe
        public virtual void CreateSlot(Vector2Int gridPos) {
            this.gridPos = gridPos;
        }

        public abstract int RemoveItem(ItemData item, int quantity = 1);
        public abstract int AddItem(ItemData item, int quantity = 1);

        public abstract bool SwapItem(ItemData item);

        public abstract bool SwapSlot(ISlotController slot);

        public abstract void Setup(ItemData item = null, int quantity = 0);
        protected abstract void HandleModelChanged(int quantity);

        public abstract void FocusSlot(bool hover);

        public abstract void SelectSlot(bool select);

        // Centraliza a desinscrição do evento do model. Evita assinatura duplicada
        // quando Setup() é chamado mais de uma vez (ex.: slot reciclado em um pool)
        // e evita repetição entre OnDestroy e Setup.
        protected void UnsubscribeFromModel() {
            if (model != null) {
                model.OnQuantityChanged -= HandleModelChanged;
            }
        }
        #endregion


    }
}