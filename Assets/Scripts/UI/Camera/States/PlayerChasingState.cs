using DG.Tweening;
using UnityEngine;

public class PlayerChasingState : State
{
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector3 _playerPositionOffset;
    [SerializeField] private Transform _playerPosition;

    private void Update()
    {
        TryFollowPlayer();
    }

    private void TryFollowPlayer()
    {
        Vector3 onPlayerPosition = _playerPosition.position + _playerPositionOffset;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(onPlayerPosition, _animationTime));
        sequence.Insert(0, transform.DOLookAt(_playerPosition.position, _animationTime));
    }
}
