using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public event Action OnDoorOpened;

    public float DoorCondition { get => doorCondition; }
    public bool Opened { get => opened; }
    private const float DOOR_CONDITION_MAX = 100F;
    private float doorCondition = DOOR_CONDITION_MAX;
    private bool opened;

    public void Cut()
    {
        doorCondition -= 34 * Time.deltaTime;
    }

    public void Break()
    {
        OnDoorOpened?.Invoke();
        
        opened = true;
        Destroy(transform.parent.gameObject);
    }
}
