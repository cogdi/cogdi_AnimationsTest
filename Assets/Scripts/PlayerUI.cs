using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public enum DialogBoxText
    {
        Intro,
        MissionEntry,
        WearMask,
        CheckDoor,
        TakeChainsaw,
        CutDoor,
        TakeExtinguisher,
        Extinguish
    }

    public static PlayerUI Instance { get; private set; }

    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject actionMessagePanel;
    [SerializeField] private TextMeshProUGUI actionMessageButtonText;
    [SerializeField] private TextMeshProUGUI actionMessageText;
    [SerializeField] private TextMeshProUGUI tutorialBoxContentText;
    [SerializeField] private TextMeshProUGUI tutorialBoxObjectiveText;

    private void Awake()
    {
        missionPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);
    }

    private void Start()
    {
        MissionManager.Instance.OnMissionStarted += MissionManager_OnMissionStarted;
    }
    
    public void DisplayTutorialBox(DialogBoxText dialogBoxText)
    {
        missionPanel.SetActive(true);

        switch (dialogBoxText)
        {
            case DialogBoxText.Intro:
                tutorialBoxContentText.text = "Перед выездом на вызов, нужно собрать недостующее снаряжение. " +
        "В данный момент в машине нет: противогаза, топора и бензопилы. Они лежат на первом этаже департамента.";
                tutorialBoxObjectiveText.text = "Цель: найти экипировку.";
                break;
            case DialogBoxText.MissionEntry:
                tutorialBoxContentText.text = "Диспетчер: загорелся деревянный дом за городом. " + 
        "Предположительно внутри была кузница, нужно выезжать поскорее.\nАдрес: [адрес].";
                tutorialBoxObjectiveText.text = "Цель: сесть в пожарную машину.";
                break;
            case DialogBoxText.WearMask:
                tutorialBoxContentText.text = "Нужно надеть маску, чтобы было легче дышать в горящем помещении. Она находится в машине.";
                tutorialBoxObjectiveText.text = "Цель: взять маску в боковом отсеке.";
                break;
            case DialogBoxText.CheckDoor:
                tutorialBoxContentText.text = "Необходимо проверить если дверь открывается, иначе придётся разрубать.";
                tutorialBoxObjectiveText.text = "Цель: проверить доступность двери.";
                break;
            case DialogBoxText.TakeChainsaw:
                tutorialBoxContentText.text = "Дверь не открывается, остаётся только рубить, воспользуюсь бензопилой. Она в машине.";
                tutorialBoxObjectiveText.text = "Цель: взять пилу в боковом отсеке.";
                break;
            case DialogBoxText.CutDoor:
                tutorialBoxContentText.text = "Чтобы рубить дверь, используйте ЛКМ.";
                tutorialBoxObjectiveText.text = "Цель: сломать дверь.";
                break;
            case DialogBoxText.TakeExtinguisher:
                tutorialBoxContentText.text = "Для тушения пожара нужно использовать шланг с высоким напором воды. Он в машине.";
                tutorialBoxObjectiveText.text = "Цель: взять шланг в боковом отсеке.";
                break;
            case DialogBoxText.Extinguish:
                tutorialBoxContentText.text = "Чтобы включить струю воду, удерживайте ЛКМ.";
                tutorialBoxObjectiveText.text = "Цель: потушить все очаги огня.";
                break;
        }
    }

    private void MissionManager_OnMissionStarted()
    {
        missionPanel.SetActive(true);
    }

    public void DisplayActionMessage(string message)
    {
        if (actionMessagePanel.activeSelf) return;

        crosshair.SetActive(false);

        actionMessageButtonText.text = PlayerInput.Instance.GetInteractInputBindingString();
        actionMessageText.text = message;
        actionMessagePanel.SetActive(true);
    
        Debug.Log("Action message showing from UI_CLASS");
    }

    public void DiscardCurrentActionMessage()
    {
        if (!actionMessagePanel.activeSelf) return;

        crosshair.SetActive(true);
        
        actionMessagePanel.SetActive(false);

        Debug.Log("Action message dissssssscarding from UI_CLASS");
    }

    public bool IsActionMessageDisplayed()
    {
        return actionMessagePanel.activeSelf;
    }
}
