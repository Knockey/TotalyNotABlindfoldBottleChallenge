using DG.Tweening;
using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector3 _mainPosition;
    [SerializeField] private Vector3 _mainRotation;
    [SerializeField] private Vector3 _playerPositionOffset;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private HitDetection _hitDetection;

    private bool _isNeedToFollowPlayer = false;

    private void OnEnable()
    {
        _hitDetection.ColliderHitedWithType += OnColliderHitedWithType;

        MoveToMainPosition();
    }

    private void OnDisable()
    {
        _hitDetection.ColliderHitedWithType -= OnColliderHitedWithType;
    }

    private void Update()
    {
        TryFollowPlayer();
    }

    private void TryFollowPlayer()
    {
        if (_isNeedToFollowPlayer)
        {
            Vector3 onPlayerPosition = _playerPosition.position + _playerPositionOffset;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(onPlayerPosition, _animationTime));
            sequence.Insert(0, transform.DOLookAt(_playerPosition.position, _animationTime));
        }
    }

    private void MoveToMainPosition()
    {
        transform.DOMove(_mainPosition, _animationTime);
        transform.DORotate(_mainRotation, _animationTime);
    }

    private void OnColliderHitedWithType(EvasionMovement type)
    {
        if (type.TryGetComponent(out PlayerEvasionMovement player))
        {
            //Vector3 onPlayerPosition = player.transform.position;
            //onPlayerPosition.x += _playerPositionOffset.x;
            //onPlayerPosition.y += _playerPositionOffset.y;
            //onPlayerPosition.z += _playerPositionOffset.z;

            //Sequence sequence = DOTween.Sequence();
            //sequence.Append(transform.DOMove(onPlayerPosition, _animationTime));
            //sequence.Insert(0, transform.DOLookAt(player.transform.position, _animationTime));

            _isNeedToFollowPlayer = true;
        }
    }
}
