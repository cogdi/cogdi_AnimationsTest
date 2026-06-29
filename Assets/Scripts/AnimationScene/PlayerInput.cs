using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action OnSwitchCameraTriggered;
    public event Action OnInteractPerformed;

    private PlayerInputActions playerInputActions;

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.OnFoot.Enable();
        playerInputActions.OnFoot.SwitchCamera.performed += SwitchCamera_Triggered;
        
        playerInputActions.OnFoot.Interact.performed += Ineract_Performed;
    }

    private void Ineract_Performed(InputAction.CallbackContext context)
    {
        OnInteractPerformed?.Invoke();
    }

    private void SwitchCamera_Triggered(InputAction.CallbackContext context)
    {
        OnSwitchCameraTriggered?.Invoke();
    }

    public Vector2 GetLookVector()
    {
        return playerInputActions.OnFoot.Look.ReadValue<Vector2>();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.OnFoot.Move.ReadValue<Vector2>().normalized;
    }
}
