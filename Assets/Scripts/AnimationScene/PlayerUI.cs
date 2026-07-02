using System;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject missionPanel;

    private void Awake()
    {
        missionPanel.SetActive(false);
    }

    private void Start()
    {
        MissionManager.Instance.OnMissionStarted += MissionManager_OnMissionStarted;
    }

    private void MissionManager_OnMissionStarted()
    {
        missionPanel.SetActive(true);
    }
}
