using UnityEngine;

public class Firetruck : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableObjectVisual visual;

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
