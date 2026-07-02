using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectTriggerManager : MonoBehaviour
{
    [SerializeField] private List<InteractableObjectTrigger> triggerList;
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
            triggerList[i].OnObjectEnteredTrigger += AssignCurrentTrigger;
            triggerList[i].OnObjectExitTrigger += DisassignCurrentTrigger;
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

        DisassignCurrentTrigger();
    }

    public void AssignCurrentTrigger(InteractableObjectTrigger trigger)
    {
        currentTrigger = trigger;
        isInsideTrigger = true;
    }
    
    public void DisassignCurrentTrigger()
    {
        currentTrigger = null;
        isInsideTrigger = false;
    }

    private void OnDestroy()
    {
        PlayerMotor.Instance.OnItemDropped -= AttachToTrigger;

        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].OnObjectEnteredTrigger -= AssignCurrentTrigger;
            triggerList[i].OnObjectExitTrigger -= DisassignCurrentTrigger;
        }   
    }
}
