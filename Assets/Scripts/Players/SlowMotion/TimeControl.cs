using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private float _slowMotionDistance;
    [SerializeField] private float _minTimeScale;
    [SerializeField] private BottleApproachHandler _bottleApproachHandler;
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
            Time.timeScale = _minTimeScale;
            return;
        }

        Time.timeScale = 1;
    }

    private void OnBottleFlewAway()
    {
        Time.timeScale = 1;
    }

    private void OnColliderHitedWithType(EvasionMovement movement)
    {
        if (movement is PlayerEvasionMovement)
            Time.timeScale = 1;
    }

    private void OnTapPanelEnabled()
    {
        Time.timeScale = 0;
    }

    private void OnPanelTaped()
    {
        Time.timeScale = 1;
    }
}
