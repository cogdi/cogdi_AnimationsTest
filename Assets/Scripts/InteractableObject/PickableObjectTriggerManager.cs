using System;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectTriggerManager : MonoBehaviour
{
    [SerializeField] private List<PickableObjectTrigger> triggerList;
    private Dictionary<PickableObjectTrigger, bool> triggerActivationDictionary;    

    private void Start()
    {
        SubscribeToEvents();
        FillDictionary();
    }

    private void FillDictionary()
    {
        triggerActivationDictionary = new Dictionary<PickableObjectTrigger, bool>();

        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerActivationDictionary[triggerList[i]] = false;
        }
    }

    private void SubscribeToEvents()
    {
        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].OnTriggerActivated += UpdateTriggerDictionary;
        }  
    }

    private void UpdateTriggerDictionary(PickableObjectTrigger trigger)
    {
        triggerActivationDictionary[trigger] = true;
        trigger.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].OnTriggerActivated -= UpdateTriggerDictionary;
        }   
    }
}
