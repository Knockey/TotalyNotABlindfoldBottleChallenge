using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTapPanel : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Fading _fadePanel;
    [SerializeField] private float _timeBeforeClick = 1f;

    private bool _isAbleToClick = false;

    private void OnEnable()
    {
        StartCoroutine(WaitBeforeClick());
    }

    private void Update()
    {
        CheckPanelTap();
    }

    private IEnumerator WaitBeforeClick()
    {
        yield return new WaitForSeconds(_timeBeforeClick);

        _isAbleToClick = true;
    }

    private void CheckPanelTap()
    {
        if (Input.GetMouseButton(0) && _isAbleToClick)
        {
            StartCoroutine(LoadWithFade());
        }
    }

    private IEnumerator LoadWithFade()
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.BecomeFullyFaded();

        PlayerPrefs.SetString("Current scene", _sceneToLoad);

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        SceneManager.LoadScene(_sceneToLoad);
    }
}
