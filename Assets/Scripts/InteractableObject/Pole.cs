using System.Collections;
using UnityEngine;

public class Pole : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform playerAttachPoint;
    [SerializeField] private Transform playerLandingPoint;

    private InteractableObjectVisual visual;
    private const string ACTION_MESSAGE = "Спуститься";
    private float interactionDistance = 5f;
    private float slideSpeed = 8.25f;

    private Transform playerTransform;

    private void Awake()
    {
        visual = GetComponent<InteractableObjectVisual>();
    }

    public string GetActionMessage()
    {
        return ACTION_MESSAGE;
    }

    public void Interact()
    {
        playerTransform = PlayerMotor.Instance.transform;

        if (Vector3.Distance(playerTransform.position, transform.position) < interactionDistance)
        {
            StartCoroutine(SlideDownPole());
        }
    }

    private IEnumerator SlideDownPole()
    {
        PlayerMotor.Instance.DisableMovement();

        playerTransform.position = playerAttachPoint.position;
        playerTransform.rotation = playerAttachPoint.rotation;

        while (Vector3.Distance(playerTransform.position, playerLandingPoint.position) > 0.02f)
        {
            playerTransform.position = Vector3.MoveTowards(
                playerTransform.position,
                playerLandingPoint.position,
                slideSpeed * Time.deltaTime);

            yield return null;
        }

        playerTransform.position = playerLandingPoint.position;

        PlayerMotor.Instance.EnableMovement();
    }

    public void HandleHighlight()
    {
        if (PlayerMotor.Instance.IsLookingAtObject)
            visual.Highlight();
        else
            visual.RemoveHighlight();
    }
}
