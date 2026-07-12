using System;
using UnityEngine;

public class PickableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InteractableObjectVisual visual;
    public bool IsHolded { get => isHolded; }

    private bool isHolded = false;
    private bool isHighlited = false;
    private const string ACTION_MESSAGE = "Взять";

    private void PickUp()
    {
        DisablePhysics();
        PlayerLook.Instance.ParentObjectToCurrentCamera(transform);
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

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }
}
