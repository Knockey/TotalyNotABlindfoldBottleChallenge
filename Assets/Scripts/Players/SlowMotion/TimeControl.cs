using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private float _slowMotionDistance;
    [SerializeField] private float _minTimeScale;
    [SerializeField] private BottleApproachHandle _bottleApproachHandler;
    [SerializeField] private HitDetection _hitDetection;
    [SerializeField] private TapPanel _tapPanel;

    private void OnEnable()
    {
        _bottleApproachHandler.BottleApproaching += OnBottleApproaching;
        _bottleApproachHandler.BottleFlewAway += OnBottleFlewAway;
        _hitDetection.ColliderHitedWithType += OnColliderHitedWithType;
        _tapPanel.PanelEnabled += OnTapPanelEnabled;
        _tapPanel.PanelTaped += OnPanelTaped;
    }

    private void OnDisable()
    {
        _bottleApproachHandler.BottleApproaching -= OnBottleApproaching;
        _bottleApproachHandler.BottleFlewAway -= OnBottleFlewAway;
        _hitDetection.ColliderHitedWithType -= OnColliderHitedWithType;
        _tapPanel.PanelEnabled -= OnTapPanelEnabled;
        _tapPanel.PanelTaped -= OnPanelTaped;
    }

    private void OnValidate()
    {
        if (_minTimeScale > 1 || _minTimeScale < 0)
            _minTimeScale = 1;
    }

    private void OnBottleApproaching(float distance)
    {
        if (distance < _slowMotionDistance)
        {
            SetTimeScale(_minTimeScale);
            return;
        }

        SetTimeScale(1f);
    }

    private void OnBottleFlewAway()
    {
        SetTimeScale(1f);
    }

    private void OnColliderHitedWithType(EvasionMovement movement)
    {
        if (movement is PlayerEvasionMovement)
            SetTimeScale(1f);
    }

    private void OnTapPanelEnabled()
    {
        SetTimeScale(0f);
    }

    private void OnPanelTaped()
    {
        SetTimeScale(1f);
    }

    private void SetTimeScale(float scaleValue)
    {
        Time.timeScale = scaleValue;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
