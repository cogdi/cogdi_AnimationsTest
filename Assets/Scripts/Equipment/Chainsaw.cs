using System;
using UnityEngine;

public class Chainsaw : PickableObject
{
    public static event Action OnAnyChainsawEquippedFirstTime;
    public override Tool ToolType { get => Tool.Chainsaw; }

    private bool isEquipped;
    private bool equippedFirstTime;
    public bool PowerMode { get; private set; }
    private DoorTrigger cuttingDoorTrigger;
    private bool isCutting;

    private void Start()
    {
        PlayerInput.Instance.OnShootButtonPressed += StartCutting;
        PlayerInput.Instance.OnShootButtonReleased += StopCutting;
    }

    private void RotateToPowerMode()
    {
        transform.localRotation = Quaternion.Euler(0, -90f, 0);
    }

    private void RotateToIdleMode()
    {
        transform.localRotation = Quaternion.Euler(0, -175f, 0);
    }

    private void StartCutting()
    {
        if (isEquipped)
        {
            PowerMode = true;
            RotateToPowerMode();
        }
    }

    private void StopCutting()
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

    private void OnTriggerEnter(Collider other)
    {
        if (!cuttingDoorTrigger)
        {
            if (other.TryGetComponent(out DoorTrigger doorTrigger))
            {
                cuttingDoorTrigger = doorTrigger;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isCutting = cuttingDoorTrigger && PowerMode;

        if (isCutting)
        {
            if (cuttingDoorTrigger.DoorCondition > 0f)
            {
                cuttingDoorTrigger.Cut();
            }

            else
            {
                Debug.Log("Door should be opened now");
                cuttingDoorTrigger.Break();
            }
        }
    }

    private void Update()
    {
        isEquipped = PlayerHands.Instance.IsObjectInHand(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (cuttingDoorTrigger)
        {
            if (other.GetComponent<DoorTrigger>())
            {
                isCutting = false;
                cuttingDoorTrigger = null;
            }
        }
    }
}
