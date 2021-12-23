using DG.Tweening;
using UnityEngine;

public class SetupState : State
{
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector3 _mainPosition;
    [SerializeField] private Vector3 _mainRotation;

    private void OnEnable()
    {
        MoveToMainPosition();
    }

    private void MoveToMainPosition()
    {
        transform.DOMove(_mainPosition, _animationTime);
        transform.DORotate(_mainRotation, _animationTime);
    }
}
