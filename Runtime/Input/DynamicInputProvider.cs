using UnityEngine;
using UnityEngine.InputSystem;
using Wellz.Inventory.Input;

[CreateAssetMenu(fileName = "DynamicInputProvider", menuName = "Wellz/Inventory/InputProvider/Dynamic")]
public class DynamicInputProvider : InputProvider {
    // Campos estáticos e constantes

    // Campos expostos no Inspector
    [Header("Input Actions (Arraste do seu PlayerControls)")]
    [SerializeField] private InputActionReference clickAction;
    [SerializeField] private InputActionReference positionAction;

    // Propriedades para acesso controlado externo

    // Campos privados para o estado interno da classe
    private void OnClickPerformed(InputAction.CallbackContext ctx) => InvokePressed();
    private void OnClickCanceled(InputAction.CallbackContext ctx) => InvokeReleased();
    private void OnPositionPerformed(InputAction.CallbackContext ctx) => InvokePositionChanged(ctx.ReadValue<Vector2>());

    #region Métodos do ciclo de vida do ScriptableObject (OnEnable, OnDisable, OnDestroy)
    public override void Activate() {
        if (clickAction != null) {
            clickAction.action.Enable();
            clickAction.action.performed += OnClickPerformed;
            clickAction.action.canceled += OnClickCanceled;
        }

        if (positionAction != null) {
            positionAction.action.Enable();
            positionAction.action.performed += OnPositionPerformed;
        }
    }

    public override void Deactivate() {
        if (clickAction != null) {
            clickAction.action.performed -= OnClickPerformed;
            clickAction.action.canceled -= OnClickCanceled;
            clickAction.action.Disable();
        }

        if (positionAction != null) {
            positionAction.action.performed -= OnPositionPerformed;
            positionAction.action.Disable();
        }
    }
    #endregion

    #region Métodos públicos e privados da lógica da classe
    public override Vector2 Position() {
        return positionAction != null ? positionAction.action.ReadValue<Vector2>() : Vector2.zero;
    }

    public override bool Pressed() {
        return clickAction != null && clickAction.action.WasPressedThisFrame();
    }

    public override bool Released() {
        return clickAction != null && clickAction.action.WasReleasedThisFrame();
    }
    #endregion
}