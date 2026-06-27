using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        PlayerMotor.Instance.OnItemPickedUp += PlayerMotor_OnItemPickedUp;
        PlayerMotor.Instance.OnItemDropped += PlayerMotor_OnItemDropped;
    }

    private void PlayerMotor_OnItemDropped(InteractableObject obj)
    {
        if (obj == this)
        {
            EnablePhysics();
            transform.SetParent(null);
        }
    }

    private void PlayerMotor_OnItemPickedUp(InteractableObject obj)
    {
        if (obj == this)
        {
            DisablePhysics();
            // transform.SetParent(PlayerMotor.Instance.transform);
            transform.SetParent(PlayerLook.Instance.FirstPersonCamera.transform);
        }
    }

    // private void PlayerMotor_OnItemPickedUpThirdPerson(InteractableObject obj)
    // {
    //     if (obj == this)
    //     {
    //         DisablePhysics();
    //         // transform.SetParent(PlayerMotor.Instance.transform);
    //         transform.SetParent(PlayerLook.Instance.FirstPersonCamera.transform);
    //     }
    // }

    private void DisablePhysics()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void EnablePhysics()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
