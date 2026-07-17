using Unity.VisualScripting;
using UnityEngine;

public class HoseWaterTrigger : MonoBehaviour
{
    private float extinguishTime;
    private const float EXTINGUISH_TIME_MAX = 3F;
    private Flamable currentFire;
    private bool extinguishing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Flamable flamable))
        {
            currentFire = flamable;
            extinguishing = true;
        }
    }

    private void Update()
    {
        if (extinguishing)
        {
            if (currentFire.FireProgress > 0f)
            {
                currentFire.FireProgress -= 34 * Time.deltaTime;
                return;
            }

            else currentFire.FireVFX.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(currentFire))
        {
            extinguishing = false;
            currentFire = null;
        }
    }
}
