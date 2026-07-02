using System;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    public event Action OnMissionStarted;

    private bool missionStarted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!missionStarted && PlayerMotor.Instance.PlayerSpeed >= 5f) // Debug logic.
        {
            OnMissionStarted?.Invoke();
        }
    }
}
