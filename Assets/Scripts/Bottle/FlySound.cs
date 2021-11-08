using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlySound : MonoBehaviour
{
    [SerializeField] private AnimationCurve _soundValue;
    [SerializeField] private ParabolicMovementState _movement;
    [SerializeField] private TapPanel _tapPanel;

    private AudioSource _flySound;
    private bool _isAbleToPlay = false;

    private void Awake()
    {
        _flySound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _tapPanel.PanelTaped += OnPanelTapped;
        _movement.RemainDistanceChanged += OnRemainDistanceChanged;
        _movement.BottleReseted += OnBottleReseted;
    }

    private void OnDisable()
    {
        _tapPanel.PanelTaped -= OnPanelTapped;
        _movement.RemainDistanceChanged -= OnRemainDistanceChanged;
        _movement.BottleReseted -= OnBottleReseted;
    }

    private void OnPanelTapped()
    {
        _flySound.Play();
        _isAbleToPlay = true;
    }

    private void OnRemainDistanceChanged(float value)
    {
        _flySound.volume = _soundValue.Evaluate(value);
    }

    private void OnBottleReseted()
    {
        if (_isAbleToPlay)
        {
            _flySound.Stop();
            _flySound.Play();
        }
    }
}
