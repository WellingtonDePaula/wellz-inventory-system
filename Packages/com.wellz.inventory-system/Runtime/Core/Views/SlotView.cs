using UnityEngine;
using UnityEngine.UI;
using Wellz.Inventory.Core.Interfaces;
using Wellz.Inventory.Items;

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

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)

        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void Clear() {
            throw new System.NotImplementedException();
        }

        public void RefreshView(int quantity) {
            throw new System.NotImplementedException();
        }

        public void SwapItem(ItemData data, int quantity) {
            throw new System.NotImplementedException();
        }

        public void SetupView(ItemData data, int quantity) {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}