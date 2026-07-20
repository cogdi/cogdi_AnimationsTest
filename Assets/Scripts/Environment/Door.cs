using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public static event Action OnAnyDoorInteractedFirstTime;
    public static event Action OnAnyDoorBroken;

    [SerializeField] private DoorTrigger trigger;
    private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Дверь заперта";
    private bool isOpen;
    private bool interactedFirstTime;

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

        OnAnyDoorBroken?.Invoke();
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
        if (!interactedFirstTime)
        {
            OnAnyDoorInteractedFirstTime?.Invoke();
            
            interactedFirstTime = true;
        }

        return;
    }
}
