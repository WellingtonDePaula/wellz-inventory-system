using System;
using UnityEngine;
using Wellz.Inventory.Core.Interfaces;

namespace Wellz.Inventory.Input {
    public abstract class InputProvider : ScriptableObject, IInputProvider {

        public event Action<Vector2> OnPositionChanged;
        public event Action OnPressed;
        public event Action OnReleased;

        protected void InvokePositionChanged(Vector2 pos) => OnPositionChanged?.Invoke(pos);
        protected void InvokePressed() => OnPressed?.Invoke();
        protected void InvokeReleased() => OnReleased?.Invoke();

        public abstract Vector2 Position();
        public abstract bool Pressed();
        public abstract bool Released();

        public virtual void Activate() { }
        public virtual void Deactivate() { }
    }
}