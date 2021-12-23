using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InitialScene : MonoBehaviour
{
    [SerializeField] private string _firstLaunchScene;
    [SerializeField] private TMP_Text _text;

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
