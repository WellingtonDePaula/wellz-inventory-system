using UnityEngine;
using UnityEngine.UI;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Items;
using Wellz.Utils.Core;

namespace Wellz.Inventory.Core.Views {
    public class SlotView : SlotViewBase {
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

        private void Awake() {
            rectTransform = backgroundImage.rectTransform;
        }

        #endregion

        #region Métodos públicos e privados da lógica da classe
        public override void Clear() {
            itemData = null;
            iconImage.sprite = null;
            quantityText.text = "";

            rectTransform.localScale = new Vector3(1, 1);
            rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public override void RefreshView(ItemData data, int quantity) {
            if(itemData != data) {
                SetupView(data, quantity);
                return;
            }
            if (itemData == null) { return; }

            quantityText.text = quantity.ToString();
        }

        public override void SwapItem(ItemData data, int quantity) {
            throw new System.NotImplementedException();
        }

        public override void SetupView(ItemData data, int quantity) {
            if (data == null) {
                Clear();
                return;
            }
            itemData = data;
            iconImage.sprite = data.Icon;
            quantityText.text = quantity.ToString();
        }

        public override void FocusStarted() {
            backgroundImage.color = UtilsClass.GetRandomColor();
            rectTransform.localScale = new Vector3(1.1f, 1.1f);
        }

        public override void FocusEnded() {
            backgroundImage.color = Color.white;
            rectTransform.localScale = new Vector3(1, 1);
        }

        public override void Select() {
            backgroundImage.color = UtilsClass.GetRandomColor();
            rectTransform.localScale = new Vector3(0.9f, 0.9f);
            rectTransform.localRotation = Quaternion.Euler(0, 0, 45);
        }

        public override void Deselect() {
            backgroundImage.color = Color.white;
            rectTransform.localScale = new Vector3(1, 1);
            rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion
    }
}