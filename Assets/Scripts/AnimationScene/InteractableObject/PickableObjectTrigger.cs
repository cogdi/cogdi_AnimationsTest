using System;
using UnityEngine;

public class PickableObjectTrigger : MonoBehaviour
{
    public event Action<PickableObjectTrigger> OnTriggerActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerMotor.Instance.IsInteractableObjectLayer(other.gameObject.layer))
        {
            if (other.TryGetComponent(out PickableObject obj))
            {
                if (!obj.IsHolded)
                {
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    OnTriggerActivated?.Invoke(this);
                }
            }
        }
    }
}
