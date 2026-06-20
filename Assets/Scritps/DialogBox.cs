using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake()
    {
        yesButton.onClick.AddListener(CloseDialogBox);
        noButton.onClick.AddListener(CloseDialogBox);
    }

    private void CloseDialogBox()
    {
        gameObject.SetActive(false);
    }
}
