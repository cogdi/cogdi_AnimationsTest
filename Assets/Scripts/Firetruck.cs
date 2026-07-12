using UnityEngine;

public class Firetruck : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Выехать на вызов";

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight(false);
        else
            visual.RemoveHighlight();
    }

    public void Interact()
    {
        Loader.Instance.LoadNextSceneByBuildIndex();
    }
}
