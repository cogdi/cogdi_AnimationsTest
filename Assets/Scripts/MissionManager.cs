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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.Intro);

        CollectibleEquipment.OnEquipmentPieceCollected += CollectibleEquipment_OnEquipmentPieceCollected;
        Loader.Instance.OnSceneChanged += Loader_OnSceneChanged;

        Chainsaw.OnAnyChainsawEquippedFirstTime += Chainsaw_OnAnyChainsawEquippedFirstTime;
        GasMask.OnAnyGasMaskEquippedFirstTime += GasMask_OnAnyGasMaskEquippedFirstTime;
        Door.OnAnyDoorInteractedFirstTime += Door_OnAnyDoorInteractedFirstTime;
        Door.OnAnyDoorBroken += Door_OnAnyDoorBroken;
        Hose.OnAnyHoseGrabbedFirstTime += Hose_OnAnyHoseGrabbedFirstTime;
    }

    private void Hose_OnAnyHoseGrabbedFirstTime()
    {
        PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.Extinguish);
    }

    private void Door_OnAnyDoorBroken()
    {
        PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.TakeExtinguisher);
    }

    private void Door_OnAnyDoorInteractedFirstTime()
    {
        PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.TakeChainsaw);
    }

    private void GasMask_OnAnyGasMaskEquippedFirstTime()
    {
        PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.CheckDoor);
    }

    private void Chainsaw_OnAnyChainsawEquippedFirstTime()
    {
        PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.CutDoor);
    }

    private void Loader_OnSceneChanged()
    {
        if (Loader.Instance.GetCurrentScene() == Loader.Scene.BurningHouse)
            PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.WearMask);
    }

    private void CollectibleEquipment_OnEquipmentPieceCollected()
    {
        if (++collectedEquipment >= missingEquipment.Length)
        {
            PlayerUI.Instance.DisplayTutorialBox(PlayerUI.DialogBoxText.MissionEntry);

            OnMissionStarted?.Invoke();
            IsMissionReady = true;
        }
    }
}
