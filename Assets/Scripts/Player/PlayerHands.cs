using UnityEngine;
using static PickableObject;

public class PlayerHands : MonoBehaviour
{
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

    public void TryAddItem(PickableObject obj)
    {
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

    private Quaternion GetToolSpecificRotation(Tool type)
    {
        switch (type)
        {
            case Tool.None:
                return Quaternion.Euler(0f, 0f, 0f);
            case Tool.Chainsaw:
                return Quaternion.Euler(0f, 90f, 0f);
            default:
                return Quaternion.Euler(0f, 0f, 0f);
        }
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
