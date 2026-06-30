using UnityEngine;
using UnityEngine.UI;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Items;
using Wellz.Utils.Core;

namespace Wellz.Inventory.Core.Views {
    public abstract class SlotViewBase : MonoBehaviour, ISlotView {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Text quantityText;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private ItemData itemData;
        private RectTransform rectTransform;

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)

        #endregion

        #region Métodos públicos e privados da lógica da classe
        public abstract void Clear();

        public abstract void RefreshView(ItemData data, int quantity);

        public abstract void SetupView(ItemData data, int quantity);

        public abstract void FocusStarted();

        public abstract void FocusEnded();

        public abstract void Select();

        public abstract void Deselect();
        #endregion
    }
}