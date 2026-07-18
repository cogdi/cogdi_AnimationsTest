using UnityEngine;

public class GasMask : MonoBehaviour, IInteractable
{
    public const string ACTION_MESSAGE = "Надеть";
    private InteractableObjectVisual visual;

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
        visual.RemoveHighlight();

        Transform headEquipmentSocket = PlayerMotor.Instance.HeadEquipmentSocket;

        transform.SetParent(headEquipmentSocket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
