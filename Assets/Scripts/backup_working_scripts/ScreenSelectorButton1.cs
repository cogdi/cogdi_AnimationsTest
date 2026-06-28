using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSelectorButton_1 : MonoBehaviour
{
    [SerializeField] public UIManager.Screen targetScreen;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OpenTargetScreen);
    }

    private void OpenTargetScreen()
    {
        UIManager.Instance.SwitchScreen(targetScreen);
    }
}
