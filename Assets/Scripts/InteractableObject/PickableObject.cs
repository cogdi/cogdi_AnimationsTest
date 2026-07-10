using System;
using NUnit.Framework;
using UnityEngine;

public class PickableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InteractableObjectVisual visual;
    public bool IsHolded { get => isHolded; }

    private bool isHolded = false;
    private bool isHighlited = false;

    private void PickUp()
    {
        DisablePhysics();
        PlayerLook.Instance.ParentObjectToCurrentCamera(transform);

        transform.position = PlayerMotor.Instance.PlayerHands.position;
    }

    private void Drop()
    {
        EnablePhysics();
        transform.SetParent(null);
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

    public void Interact()
    {
        if (isHolded)
        {
            visual.RemoveHighlight();
            Drop();
        }

        else
        {
            visual.Highlight(true);
            PickUp();
        }

        isHolded = !isHolded;
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight(isHolded);
        else
            visual.RemoveHighlight();
    }
}
