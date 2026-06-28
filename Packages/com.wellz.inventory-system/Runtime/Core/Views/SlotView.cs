using UnityEngine;
using UnityEngine.UI;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Items;
using Wellz.Utils.Core;

namespace Wellz.Inventory.Core.Views {
    public class SlotView : MonoBehaviour, ISlotView {
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

        private void Start() {
            rectTransform = backgroundImage.rectTransform;
        }

        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void Clear() {
            throw new System.NotImplementedException();
        }

        public void RefreshView(ItemData data, int quantity) {
            if(itemData != data) {
                SetupView(data, quantity);
                return;
            }
            if (itemData == null) { return; }

            quantityText.text = quantity.ToString();
        }

        public void SwapItem(ItemData data, int quantity) {
            throw new System.NotImplementedException();
        }

        public void SetupView(ItemData data, int quantity) {
            if (data == null) {
                itemData = null;
                iconImage.sprite = null;
                quantityText.text = "";
                return;
            }
            itemData = data;
            iconImage.sprite = data.Icon;
            quantityText.text = quantity.ToString();
        }

        public void HoverEnter() {
            backgroundImage.color = UtilsClass.GetRandomColor();
            rectTransform.localScale = new Vector3(1.1f, 1.1f);
        }

        public void HoverExit() {
            backgroundImage.color = Color.white;
            rectTransform.localScale = new Vector3(1, 1);
        }

        public void Select() {
            backgroundImage.color = UtilsClass.GetRandomColor();
            rectTransform.localScale = new Vector3(1.1f, 1.1f);
        }

        public void Deselect() {
            backgroundImage.color = Color.white;
            rectTransform.localScale = new Vector3(1, 1);
        }
        #endregion
    }
}