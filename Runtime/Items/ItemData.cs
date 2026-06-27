using System;
using UnityEngine;

namespace Wellz.Inventory.Items {
    [CreateAssetMenu(fileName = "ItemData", menuName = "Wellz/Inventory/ItemData")]
    public class ItemData : ScriptableObject {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        public string ItemId;
        public string DisplayName;
        public string Description;
        public Sprite Icon;

        public float Price;

        public bool IsStackable;
        [Min(0)]
        public int MaxStackSize;
        public bool IsPermanentSlot = false;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida do ScriptableObject (OnEnable, OnDisable, OnDestroy)

        [ContextMenu("Generate Item ID")]
        private void GenerateId() {
            ItemId = Guid.NewGuid().ToString();

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private void OnValidate() {
            if (string.IsNullOrEmpty(ItemId)) {
                GenerateId();
            }
            if (IsPermanentSlot) {
                IsStackable = true;
            }
            if (!IsStackable) {
                MaxStackSize = 1;
            }
        }

        #endregion

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
