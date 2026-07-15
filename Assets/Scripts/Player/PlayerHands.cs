using System;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    //public event Action OnObjectTakenInHands;

    public static PlayerHands Instance { get; private set; }

    private PickableObject holdedItem;
    public bool HandsOccupied { get => handsOccupied; } 
    private bool handsOccupied;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        PickableObject.OnObjectPickedUp += TryAddItem;
    }

    public void TryAddItem(PickableObject obj)
    {
        // if (handsOccupied)
        //     return;
        if (holdedItem == obj)
            return;
        
        Debug.Log("Trying to add an item");

        PlayerUI.Instance.DiscardCurrentActionMessage();

        holdedItem = obj;

        holdedItem.DisablePhysics();
        holdedItem.transform.SetParent(transform);
        holdedItem.transform.position = transform.position;

        handsOccupied = true;
    }

    public void DropItem()
    {
        Debug.Log("Dropping PO");

        holdedItem.EnablePhysics();
        holdedItem.transform.SetParent(null);

        holdedItem = null;
        handsOccupied = false;
    }

    public bool IsObjectInHand(IInteractable item)
    {
        return holdedItem == (UnityEngine.Object)item;
    }
}
