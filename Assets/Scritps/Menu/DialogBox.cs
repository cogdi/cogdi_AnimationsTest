using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        yesButton.onClick.AddListener(CloseDialogBox);
        noButton.onClick.AddListener(CloseDialogBox);
        closeButton.onClick.AddListener(CloseDialogBox);
    }

    private void CloseDialogBox()
    {
        gameObject.SetActive(false);
    }
}
