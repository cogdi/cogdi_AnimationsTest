using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectTriggerManager : MonoBehaviour
{
    [SerializeField] private List<InteractableObjectTrigger> triggerList;
    //private GameObject lastInteractedTrigger; 
    private InteractableObjectTrigger currentTrigger;
    private Dictionary<InteractableObjectTrigger, bool> triggerActivationDictionary;
    
    private bool isInsideTrigger;

    private void Start()
    {
        SubscribeToEvents();
        FillDictionary();
    }

    private void FillDictionary()
    {
        triggerActivationDictionary = new Dictionary<InteractableObjectTrigger, bool>();

        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerActivationDictionary[triggerList[i]] = false;
        }
    }

    private void SubscribeToEvents()
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
        if (!currentTrigger)
            return;

        obj.transform.position = currentTrigger.transform.position;
        obj.transform.rotation = currentTrigger.transform.rotation;

        triggerActivationDictionary[currentTrigger] = true;
        currentTrigger.gameObject.SetActive(false);

        currentTrigger = null;
        isInsideTrigger = false;
    }

    public void TriggerList_OnObjectEnteredTrigger(InteractableObjectTrigger trigger)
    {
        currentTrigger = trigger;
        isInsideTrigger = true;
    }
    
    public void TriggerList_OnObjectExitTrigger()
    {
        currentTrigger = null;
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
