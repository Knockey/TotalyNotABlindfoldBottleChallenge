using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fading : MonoBehaviour
{
    [SerializeField] private float _absoluteFadeValue;
    [SerializeField] private float _animationTime;

    private Image _image;

    public float AnimationTime => _animationTime;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnValidate()
    {
        if (_absoluteFadeValue > 1f || _absoluteFadeValue < 0f)
        {
            _absoluteFadeValue = 1f;
        }
    }

    private void Start()
    {
        StartCoroutine(BecomeFullyVisible());
    }

    public void BecomeFullyFaded()
    {
        _image.raycastTarget = true;
        _image.DOFade(_absoluteFadeValue, _animationTime);
    }

    public IEnumerator BecomeFullyVisible()
    {
        _image.DOFade(0f, _animationTime);

        yield return new WaitForSeconds(_animationTime);

        _image.raycastTarget = false;
    }
}