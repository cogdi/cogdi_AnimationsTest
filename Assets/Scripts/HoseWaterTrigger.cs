using Unity.VisualScripting;
using UnityEngine;

public class HoseWaterTrigger : MonoBehaviour
{
    private FireTrigger currentFire;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FireTrigger flamable))
        {
            currentFire = flamable;
        }
    }

    private void Update()
    {
        if (currentFire)
        {
            if (currentFire.FireProgress > 0f)
            {
                currentFire.Extinguish();
            }

            else
            {
                currentFire.StopBurning();
                currentFire = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(currentFire.gameObject))
        {
            currentFire = null;
        }
    }
}
