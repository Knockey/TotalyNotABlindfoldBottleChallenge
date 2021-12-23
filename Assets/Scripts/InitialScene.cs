using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    [SerializeField] private string _firstLaunchScene;

    private void OnEnable()
    {
        var currentLevelName = PlayerPrefs.GetString("Current scene");

        if (String.IsNullOrEmpty(currentLevelName))
        {
            SceneManager.LoadScene(_firstLaunchScene);
            return;
        }

        SceneManager.LoadScene(currentLevelName);
    }
}
