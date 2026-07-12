using UnityEngine;

public class Pole : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform playerAttachPoint;
    [SerializeField] private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Спуститься";

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }

    public void Interact()
    {
        Debug.Log("Interacting with a pole");
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight(false);
        else
            visual.RemoveHighlight();
    }
}
