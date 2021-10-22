using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private Object _sceneToLoad;

    private Button _restartButton;

    private void Awake()
    {
        _restartButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnLoadLevelButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnLoadLevelButtonClick);
    }
    private void OnLoadLevelButtonClick()
    {
        SceneManager.LoadScene(_sceneToLoad.name);
    }
}
