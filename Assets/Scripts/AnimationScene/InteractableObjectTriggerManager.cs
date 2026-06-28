using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectTriggerManager : MonoBehaviour
{
    [SerializeField] private List<InteractableObjectTrigger> triggerList;
    private GameObject lastInteractedTrigger; 
    // private Transform lastInteractedObject;
    private bool isInsideTrigger;

    private void Start()
    {
        PlayerMotor.Instance.OnItemDropped += AttachToTrigger;

        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].OnObjectEnteredTrigger += TriggerList_OnObjectEnteredTrigger;
            triggerList[i].OnObjectExitTrigger += TriggerList_OnObjectExitTrigger;
        }   
    }

    public void AttachToTrigger(InteractableObject obj)
    {
        if (!lastInteractedTrigger)
            return;

        //lastInteractedObject = obj.gameObject.transform;
        obj.transform.position = lastInteractedTrigger.transform.position;
        obj.transform.rotation = lastInteractedTrigger.transform.rotation;
        lastInteractedTrigger = null;
        isInsideTrigger = false;
    }

    public void TriggerList_OnObjectEnteredTrigger(GameObject trigger)
    {
        lastInteractedTrigger = trigger;
        isInsideTrigger = true;
    }
    
    public void TriggerList_OnObjectExitTrigger()
    {
        lastInteractedTrigger = null;
        isInsideTrigger = false;
    }

    private void OnDestroy()
    {
        PlayerMotor.Instance.OnItemDropped -= AttachToTrigger;

        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].OnObjectEnteredTrigger -= TriggerList_OnObjectEnteredTrigger;
            triggerList[i].OnObjectExitTrigger -= TriggerList_OnObjectExitTrigger;
        }   
    }
}
