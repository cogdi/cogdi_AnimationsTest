using System;
using UnityEngine;

public class Chainsaw : PickableObject
{
    public static event Action OnAnyChainsawEquippedFirstTime;
    public override Tool ToolType { get => Tool.Chainsaw; }

    private bool isEquipped;
    private bool equippedFirstTime;
    public bool PowerMode { get; private set; }

    private void Start()
    {
        PlayerInput.Instance.OnShootButtonPressed += Start_Cutting;
        PlayerInput.Instance.OnShootButtonReleased += Stop_Cutting;
    }

    private void Update()
    {
        isEquipped = PlayerHands.Instance.IsObjectInHand(this);
    }

    private void RotateToPowerMode()
    {
        transform.localRotation = Quaternion.Euler(0, -90f, 0);
    }

    private void RotateToIdleMode()
    {
        transform.localRotation = Quaternion.Euler(0, -175f, 0);
    }

    private void Start_Cutting()
    {
        if (isEquipped)
        {
            PowerMode = true;
            RotateToPowerMode();
        }
    }

    private void Stop_Cutting()
    {
        if (isEquipped)
        {
            PowerMode = false;
            RotateToIdleMode();
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (!equippedFirstTime)
        {
            OnAnyChainsawEquippedFirstTime?.Invoke();
            equippedFirstTime = true;
        }
    }
}
