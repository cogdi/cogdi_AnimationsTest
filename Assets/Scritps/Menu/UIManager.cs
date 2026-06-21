using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public enum Screen
    {
        Licenses,
        Exercises,
        News
    }

    [SerializeField] private GameObject LicensesScreen;
    [SerializeField] private GameObject ExercisesScreen;
    [SerializeField] private GameObject NewsScreen;

    [SerializeField] private Button LicensesScreenButton;
    [SerializeField] private Button ExercisesScreenButton;
    [SerializeField] private Button NewsScreenButton;
    [SerializeField] private Color buttonDefaultColor;
    [SerializeField] private Color buttonSelectedColor;

    private Dictionary<Screen, GameObject> screenPanelDictionary;
    private Dictionary<Screen, Button> screenButtonDictionary;
    private Screen? currentScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        screenPanelDictionary = new Dictionary<Screen, GameObject>
        {
            { Screen.Licenses, LicensesScreen },
            { Screen.Exercises, ExercisesScreen },
            { Screen.News, NewsScreen }
        };

        screenButtonDictionary = new Dictionary<Screen, Button>
        {
            { Screen.Licenses, LicensesScreenButton },
            { Screen.Exercises, ExercisesScreenButton },
            { Screen.News, NewsScreenButton }
        };
    }

    private void Start()
    {
        ResetButtonColors();
        SetSelectedButtonColor(screenButtonDictionary[Screen.Licenses]);
        SwitchScreen(Screen.Licenses);
    }

    public void SwitchScreen(Screen screen)
    {
        if (currentScreen == screen)
        {
            Debug.Log("CURRENT SCREEN IS LICEN");
            return;
        }

        CloseAllScreens();
        ResetButtonColors();
        SetSelectedButtonColor(screenButtonDictionary[screen]);

        screenPanelDictionary[screen].SetActive(true);
        currentScreen = screen;
    }

    private void SetSelectedButtonColor(Button button)
    {
        button.GetComponent<Image>().color = buttonSelectedColor;
    }

    private void ResetButtonColors()
    {
        foreach (var item in screenButtonDictionary)
        {
            item.Value.GetComponent<Image>().color = buttonDefaultColor;
        }
    }

    private void CloseAllScreens()
    {
        LicensesScreen.SetActive(false);
        ExercisesScreen.SetActive(false);
        NewsScreen.SetActive(false);
    }
}
