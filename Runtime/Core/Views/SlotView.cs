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
        private bool selected = false;
        private bool hover = false;

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
            hover = true;
            if (selected) { return; }
            backgroundImage.color = UtilsClass.GetRandomColor();
            rectTransform.localScale = new Vector3(1.1f, 1.1f);
        }

        public void HoverExit() {
            hover = false;
            if (selected) { return; }
            backgroundImage.color = Color.white;
            rectTransform.localScale = new Vector3(1, 1);
        }

        public void Select() {
            selected = true;
            backgroundImage.color = UtilsClass.GetRandomColor();
            rectTransform.localScale = new Vector3(1.1f, 1.1f);
        }

        public void Deselect() {
            selected = false;
            backgroundImage.color = Color.white;
            rectTransform.localScale = new Vector3(1, 1);

            if (hover) {
                backgroundImage.color = UtilsClass.GetRandomColor();
                rectTransform.localScale = new Vector3(1.1f, 1.1f);
            }
        }
        #endregion
    }
}