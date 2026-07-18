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
    private Chainsaw chainsaw;
    private bool isCutting;

    private void OnTriggerEnter(Collider other)
    {
        if (!opened)
        {
            if (!chainsaw)
            {
                if (other.TryGetComponent(out Chainsaw chainsaw))
                {
                    this.chainsaw = chainsaw;
                    isChainsawInsideTrigger = true;
                }
            }

            else if (other.gameObject.Equals(chainsaw.gameObject))
            {
                isChainsawInsideTrigger = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isCutting = chainsaw && isChainsawInsideTrigger && chainsaw.PowerMode;
    }

    private void Update()
    {
        if (!opened && isCutting)
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
                isCutting = false;
                isChainsawInsideTrigger = false;
            }
        }
    }
}
