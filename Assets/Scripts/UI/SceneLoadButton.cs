using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private Object _sceneToLoad;
    [SerializeField] private Fading _fadePanel;

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
        StartCoroutine(LoadWithFade());
    }

    private IEnumerator LoadWithFade()
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.BecomeFullyFaded();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        SceneManager.LoadScene(_sceneToLoad.name);
    }
}
