using System;
using UnityEngine;

public class GasMask : MonoBehaviour, IInteractable
{
    public static event Action OnAnyGasMaskEquippedFirstTime;

    public const string ACTION_MESSAGE = "Надеть";
    private InteractableObjectVisual visual;
    private Rigidbody rb;

    private void Awake()
    {
        visual = GetComponent<InteractableObjectVisual>();
        rb = GetComponent<Rigidbody>();
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

    public void DisablePhysics()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void Interact()
    {
        visual.RemoveHighlight();
        DisablePhysics();

        Transform headEquipmentSocket = PlayerMotor.Instance.HeadEquipmentSocket;

        transform.SetParent(headEquipmentSocket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        OnAnyGasMaskEquippedFirstTime?.Invoke();
    }
}
