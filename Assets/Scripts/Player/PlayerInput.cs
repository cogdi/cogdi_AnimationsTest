using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event Action OnSwitchCameraPerformed;
    public event Action OnInteractPerformed;
    public event Action OnShootButtonPressed;
    public event Action OnShootButtonReleased;

    private PlayerInputActions playerInputActions;

    private const string KEYBOARD_BINDING_GROUP = "Keyboard&Mouse";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.OnFoot.Enable();
        playerInputActions.OnFoot.SwitchCamera.performed += SwitchCamera_Performed;
        
        playerInputActions.OnFoot.Interact.performed += Interact_Performed;
        playerInputActions.OnFoot.Shoot.started += Shoot_Started;
        playerInputActions.OnFoot.Shoot.canceled += Shoot_Canceled;
    }

    private void Shoot_Started(InputAction.CallbackContext context)
    {
        OnShootButtonPressed?.Invoke();
    }

    private void Shoot_Canceled(InputAction.CallbackContext context)
    {
        OnShootButtonReleased?.Invoke();
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
        OnInteractPerformed?.Invoke();
    }

    private void SwitchCamera_Performed(InputAction.CallbackContext context)
    {
        OnSwitchCameraPerformed?.Invoke();
    }

    public Vector2 GetLookVector()
    {
        return playerInputActions.OnFoot.Look.ReadValue<Vector2>();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.OnFoot.Move.ReadValue<Vector2>().normalized;
    }

    public bool IsRunKeyHolded()
    {
        return playerInputActions.OnFoot.Run.IsPressed();
    }

    public string GetInteractInputBindingString()
    {
        return playerInputActions.OnFoot.Interact.GetBindingDisplayString(group: KEYBOARD_BINDING_GROUP);
    }
}
