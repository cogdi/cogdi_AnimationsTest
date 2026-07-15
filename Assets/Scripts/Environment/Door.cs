using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Дверь заперта";
    private bool isOpen;

    private void Awake()
    {
        visual = GetComponent<InteractableObjectVisual>();
    }

    public string GetActionMessage()
    {
        if (isOpen)
            return null;
        else
            return ACTION_MESSAGE;
    }

    public void HandleHighlight()
    {
        if (isOpen)
            return;
        else
            visual.Highlight();
    }

    public void Interact()
    {
        Debug.Log("Cue to have cut the lock with chainsaw");
        return;
    }
}
