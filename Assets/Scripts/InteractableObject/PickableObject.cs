using UnityEngine;

public abstract class PickableObject : MonoBehaviour, IInteractable
{
    public enum Tool
    {
        // Type None is set when a pickable object is not an part of equipment (FiguresScene).
        None,
        Chainsaw,
        Hose,
    }

    public abstract Tool ToolType { get; }
    protected Rigidbody rb;
    protected InteractableObjectVisual visual;
    protected Transform playerHands;
    public bool IsHolded { get => isHolded; }

    protected bool isHolded = false;
    protected bool isHighlited = false;
    protected const string ACTION_MESSAGE = "Взять";

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        visual = GetComponent<InteractableObjectVisual>();
    }

    public void DisablePhysics()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted");

        visual.RemoveHighlight();
        PlayerHands.Instance.TryAddItem(this);
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight();

        else
            visual.RemoveHighlight();
    }

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }
}
