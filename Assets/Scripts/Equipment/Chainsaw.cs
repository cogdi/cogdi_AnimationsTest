using UnityEngine;

public class Chainsaw : PickableObject
{
    public override Tool ToolType { get => Tool.Chainsaw; }

    // [SerializeField] private ParticleSystem particles;
    // [SerializeField] private GameObject waterTrigger;

    private bool isEquipped;
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
}
