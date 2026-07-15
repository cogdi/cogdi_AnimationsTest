using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Chainsaw>())
            Debug.Log("CHAINSOOOO MAAAAN");
    }
}
