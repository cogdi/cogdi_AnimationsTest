using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event Action OnSwitchCameraTriggered;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.OnFoot.Enable();
        playerInputActions.OnFoot.SwitchCamera.performed += SwitchCamera_Triggered;
    }

    private void SwitchCamera_Triggered(InputAction.CallbackContext context)
    {
        OnSwitchCameraTriggered?.Invoke();
    }

    public Vector2 GetLookVectorNormalized()
    {
        return playerInputActions.OnFoot.Look.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.OnFoot.Move.ReadValue<Vector2>().normalized;
    }
}
