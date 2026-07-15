using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject actionMessagePanel;
    [SerializeField] private TextMeshProUGUI actionMessageButtonText;
    [SerializeField] private TextMeshProUGUI actionMessageText;

    private void Awake()
    {
        missionPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        MissionManager.Instance.OnMissionStarted += MissionManager_OnMissionStarted;
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
