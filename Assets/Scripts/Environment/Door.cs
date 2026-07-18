using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorTrigger trigger;
    private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Дверь заперта";
    private bool isOpen;

    private void Awake()
    {
        visual = GetComponent<InteractableObjectVisual>();
    }

    private void Start()
    {
        trigger.OnDoorOpened += DoorTrigger_OnDoorOpened;
    }

    private void DoorTrigger_OnDoorOpened()
    {
        isOpen = true;
        gameObject.SetActive(false);
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
