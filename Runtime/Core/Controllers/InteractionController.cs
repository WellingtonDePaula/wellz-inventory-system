using System;
using System.Collections.Generic;
using System.Text;
using Wellz.Inventory.Core.Interfaces;

namespace Wellz.Inventory.Core.Controllers {
    public class InteractionController : IInteractionController {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        protected IInputProvider input;

        public InteractionController(IInputProvider inputProvider) {
            input = inputProvider;
        }

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void Deselect() {
            throw new NotImplementedException();
        }

        public void Select() {
            throw new NotImplementedException();
        }

        public void HoverEnter() {
            throw new NotImplementedException();
        }

        public void HoverExit() {
            throw new NotImplementedException();
        }
        #endregion

    }
}
