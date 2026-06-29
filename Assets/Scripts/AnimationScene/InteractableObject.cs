using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InteractableObjectVisual visual;

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
            visual.RemoveHighlight();
        }
    }

    private void PlayerMotor_OnItemPickedUp(InteractableObject obj)
    {
        if (obj == this)
        {
            DisablePhysics();
            PlayerLook.Instance.ParentObjectToCurrentCamera(transform);
            visual.HighlightObjectHolded();
        }
    }


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
