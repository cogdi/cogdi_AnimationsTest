using System.Collections.Generic;
using UnityEngine;

public class UIManager_1 : MonoBehaviour
{
    public static UIManager_1 Instance { get; private set; }

    public enum Screen
    {
        Licenses,
        Exercises,
        News
    }

    [SerializeField] private GameObject LicensesScreen;
    [SerializeField] private GameObject ExercisesScreen;
    [SerializeField] private GameObject NewsScreen;
    private Screen currentScreen;
    private Dictionary<Screen, GameObject> screenPanelDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SwitchScreen(Screen.Licenses);

        screenPanelDictionary = new Dictionary<Screen, GameObject>
        {
            { Screen.Licenses, LicensesScreen },
            { Screen.Exercises, ExercisesScreen },
            { Screen.News, NewsScreen }
        };
    }

    public void SwitchScreen(Screen screen)
    {
        if (currentScreen == screen)
        {
            return;
        }

        CloseAllScreens();

        screenPanelDictionary[screen].SetActive(true);
        currentScreen = screen;
    }

    private void CloseAllScreens()
    {
        LicensesScreen.SetActive(false);
        ExercisesScreen.SetActive(false);
        NewsScreen.SetActive(false);
    }
}
