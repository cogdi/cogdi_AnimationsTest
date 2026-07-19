using System;
using UnityEngine;

public class CollectibleEquipment : MonoBehaviour, IInteractable
{
    public static event Action OnEquipmentPieceCollected;

    public const string ACTION_MESSAGE = "Положить в машину";
    protected InteractableObjectVisual visual;

    private void Awake()
    {
        visual = GetComponent<InteractableObjectVisual>();
    }

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight();

        else
            visual.RemoveHighlight();
    }

    public void Interact()
    {
        OnEquipmentPieceCollected?.Invoke();

        gameObject.SetActive(false);
    }
}
