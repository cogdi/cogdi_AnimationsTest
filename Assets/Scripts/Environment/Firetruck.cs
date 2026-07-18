using UnityEngine;

public class Firetruck : MonoBehaviour, IInteractable
{
    private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Выехать на вызов";

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
        Loader.Instance.LoadNextSceneByBuildIndex();
    }
}
