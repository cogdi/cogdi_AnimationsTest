using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InteractableObjectVisual visual;

    private void Start()
    {
        PlayerMotor.Instance.OnItemPickedUpFirstPerson += PlayerMotor_OnItemPickedUpFirstPerson;
        PlayerMotor.Instance.OnItemPickedUpThirdPerson += PlayerMotor_OnItemPickedUpThirdPerson;
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

    private void PlayerMotor_OnItemPickedUpFirstPerson(InteractableObject obj)
    {
        if (obj == this)
        {
            DisablePhysics();
            PlayerLook.Instance.ParentObjectToFirstPersonCamera(transform);
            visual.HighlightObjectHolded();
        }
    }

    private void PlayerMotor_OnItemPickedUpThirdPerson(InteractableObject obj)
    {
        if (obj == this)
        {
            DisablePhysics();
            PlayerLook.Instance.ParentObjectToThirdPersonCamera(transform);
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
