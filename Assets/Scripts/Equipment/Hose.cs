using System;
using UnityEngine;

public class Hose : PickableObject
{
    public override Tool ToolType { get => Tool.Hose; }

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject waterTrigger;

    private bool isEquipped;

    private void Start()
    {
        PlayerInput.Instance.OnShootButtonPressed += Start_Watering;
        PlayerInput.Instance.OnShootButtonReleased += Stop_Watering;
    }

    private void Update()
    {
        isEquipped = PlayerHands.Instance.IsObjectInHand(this);
    }

    private void Start_Watering()
    {
        if (isEquipped)
        {
            particles.gameObject.SetActive(true);
            waterTrigger.SetActive(true);
        }
    }

    private void Stop_Watering()
    {
        if (isEquipped)
        {
            particles.gameObject.SetActive(false);
            waterTrigger.SetActive(false);
        }
    }
}
