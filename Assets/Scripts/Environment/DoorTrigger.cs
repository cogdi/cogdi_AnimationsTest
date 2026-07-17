using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public event Action OnDoorOpened;

    //[SerializeField] private Door door;
    private bool isChainsawInsideTrigger;
    private const float CHAINSAW_PROGRESS_MAX = 100F;
    private float chainsawProgress;
    private bool opened;

    private void OnTriggerEnter(Collider other)
    {
        if (!opened)
        {
            if (other.GetComponent<Chainsaw>())
            {
                isChainsawInsideTrigger = true;
            }
        }
    }
    
    private void Update()
    {
        if (!opened && isChainsawInsideTrigger)
        {
            if (chainsawProgress <= CHAINSAW_PROGRESS_MAX)
            {
                chainsawProgress += 20 * Time.deltaTime;
            }

            else
            {
                Debug.Log("Door should be opened now");

                opened = true;
                OnDoorOpened?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!opened)
        {
            if (other.GetComponent<Chainsaw>())
            {
                isChainsawInsideTrigger = false;
            }
        }
    }
}
