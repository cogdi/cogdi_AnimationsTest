using UnityEngine;

public class FoldedHose : MonoBehaviour, IInteractable
{
    private const string ACTION_MESSAGE = "Взять шланг";
    private const string HOSE_PREFAB_PATH = "Prefabs/Hose";

    private InteractableObjectVisual visual;

    private void Awake()
    {
        visual = GetComponent<InteractableObjectVisual>();
    }

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight();
        else
            visual.RemoveHighlight();
    }

    public void Interact()
    {
        AddHose();
    }

    private void AddHose()
    {
        if (Instantiate(Resources.Load(HOSE_PREFAB_PATH) as GameObject).TryGetComponent(out Hose hose))
        {
            PlayerHands.Instance.TryAddItem(hose);
        }
    }
}