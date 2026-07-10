using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static Loader Instance { get; private set; }

    private int sceneCount;

    private void Awake()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    public void LoadNextSceneByBuildIndex()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Changing scene...");
        if (buildIndex < sceneCount - 1)
        {
            SceneManager.LoadSceneAsync(++buildIndex);
        }
        
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
