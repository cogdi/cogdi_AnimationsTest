using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Entered door trigger");
    }
}
