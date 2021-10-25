using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector3 _mainPosition;
    [SerializeField] private Vector3 _mainRotation;
    [SerializeField] private Vector3 _playerPositionOffset;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private HitDetection _hitDetection;

    private void OnEnable()
    {
        _hitDetection.ColliderHitedWithType += OnColliderHitedWithType;

        MoveToMainPosition();
    }

    private void OnDisable()
    {
        _hitDetection.ColliderHitedWithType -= OnColliderHitedWithType;
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
            Vector3 onPlayerPosition = player.transform.position;
            onPlayerPosition.x += _playerPositionOffset.x;
            onPlayerPosition.y += _playerPositionOffset.y;
            onPlayerPosition.z += _playerPositionOffset.z;

            transform.DOMove(onPlayerPosition, _animationTime);
            transform.DOLookAt(player.transform.position, _animationTime);
        }
    }
}
