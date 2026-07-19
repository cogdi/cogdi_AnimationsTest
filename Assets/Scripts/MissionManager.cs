using System;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    // public event Action OnMissionReady;
    public event Action OnMissionStarted;

    [SerializeField] private CollectibleEquipment[] missingEquipment;
    private int collectedEquipment;
    public bool IsMissionReady { get; private set; }


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
        CollectibleEquipment.OnEquipmentPieceCollected += CollectibleEquipment_OnEquipmentPieceCollected;
    }

    private void CollectibleEquipment_OnEquipmentPieceCollected()
    {
        if (++collectedEquipment >= missingEquipment.Length)
        {
            OnMissionStarted?.Invoke();
            IsMissionReady = true;
        }
    }
}
