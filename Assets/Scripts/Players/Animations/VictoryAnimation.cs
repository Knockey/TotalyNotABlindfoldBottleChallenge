using DG.Tweening;
using UnityEngine;

public class VictoryAnimation : MonoBehaviour
{
    private const float MinimalAnimationTime = 0.6f;

    [SerializeField] private Animator _animator;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Transform _dancePosition;
    [SerializeField] private float _standUpAnimationTime = 1.5f;
    [SerializeField] private float _dancePositionMoveTime = 1f;
    [SerializeField] private float YOffset;

    [SerializeField] protected PlayersConditionWatchdog Watchdog;

    private void OnValidate()
    {
        if (_standUpAnimationTime < MinimalAnimationTime)
            _standUpAnimationTime = MinimalAnimationTime;
    }

    protected void Animate()
    {
        _animator.enabled = true;

        Sequence standUpSequence = DOTween.Sequence();

        transform.localPosition = _initialPosition;
        Vector3 standUpPosition = _dancePosition.position;
        standUpPosition.y += YOffset;

        standUpSequence.Append(transform.DOMove(standUpPosition, _standUpAnimationTime));
        standUpSequence.Append(transform.DOMove(_dancePosition.position, _dancePositionMoveTime));
    }
}
