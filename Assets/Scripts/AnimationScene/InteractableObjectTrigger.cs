using System;
using UnityEngine;

public class InteractableObjectTrigger : MonoBehaviour
{
    public event Action<InteractableObjectTrigger> OnObjectEnteredTrigger;
    public event Action OnObjectExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(PlayerMotor.Instance.IsInteractableObjectLayer(other.gameObject.layer))
        {
            OnObjectEnteredTrigger?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(PlayerMotor.Instance.IsInteractableObjectLayer(other.gameObject.layer))
        {
            OnObjectExitTrigger?.Invoke();
        }
    }
}
