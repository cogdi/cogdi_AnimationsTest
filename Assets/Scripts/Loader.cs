using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public enum Scene
    {
        FireDepartment,
        BurningHouse
    }

    public event Action OnSceneChanged;

    public static Loader Instance { get; private set; }

    private int sceneCount;

    private Scene? currentScene;

    private void Awake()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        
        RefreshCurrentScene(SceneManager.GetActiveScene().name);

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_SceneLoaded;
    }

    private void SceneManager_SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode arg1)
    {
        RefreshCurrentScene(scene.name);
        OnSceneChanged?.Invoke();
    }

    private void RefreshCurrentScene(string sceneName)
    {
        Debug.Log(sceneName);
        switch (sceneName)
        {
            case "FireDepartment":
                currentScene = Scene.FireDepartment;
                break;
            case "BurningHouse":
                currentScene = Scene.BurningHouse;
                break;
            default:
                Debug.LogError("Current scene is unknown. Update the \"Loader.Scene\" enum.");
                currentScene = null;
                break;
        }
    }

    public void LoadNextSceneByBuildIndex()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Changing scene...");
        if (index < sceneCount - 1)
        {
            SceneManager.LoadScene(++index);

        }
        
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public Scene? GetCurrentScene()
    {
        return currentScene;
    }
}
