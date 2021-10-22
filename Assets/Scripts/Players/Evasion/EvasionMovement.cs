using System;
using UnityEngine;

public class EvasionMovement : MonoBehaviour
{
    [SerializeField] private Transform _upperTorso;
    [SerializeField] private float _maxEvasionDistance;
    [SerializeField] private float _speed;
    [SerializeField] private float _returnSpeed;

    private Vector3 _headColliderCenter;
    private Vector3 _upperTorsoCenter;
    private Vector3 _currentEvasionDirection;
    private float _currentReturnSpeed;

    private void Awake()
    {
        _headColliderCenter = transform.position;
        _upperTorsoCenter = _upperTorso.position;
        _currentReturnSpeed = _returnSpeed;
    }

    protected virtual void Update()
    {
        SetReturnSpeed();
        TryReturnToCenterPosition();
    }

    protected void TryMove(Vector3 direction)
    {
        _currentEvasionDirection = direction;
        Vector3 nextPositionOffset = _speed * Time.fixedDeltaTime * direction;

        transform.position = _headColliderCenter + GetEvasionDistance(_headColliderCenter, transform.position, nextPositionOffset);
        _upperTorso.position = _upperTorsoCenter + GetEvasionDistance(_upperTorsoCenter, _upperTorso.position, nextPositionOffset);
    }

    private void SetReturnSpeed()
    {
        if (_currentEvasionDirection == Vector3.zero)
        {
            _currentReturnSpeed = _returnSpeed;
            return;
        }

        _currentReturnSpeed = 0f;
    }

    private void TryReturnToCenterPosition()
    {
        if (_headColliderCenter != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _headColliderCenter, _currentReturnSpeed * Time.deltaTime);
            _upperTorso.position = Vector3.MoveTowards(_upperTorso.position, _upperTorsoCenter, _currentReturnSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetEvasionDistance(Vector3 center, Vector3 currentPosition, Vector3 nextPositionOffset)
    {
        var nextPosition = nextPositionOffset + currentPosition;
        Vector3 offset = nextPosition - center;

        return Vector3.ClampMagnitude(offset, _maxEvasionDistance);
    }
}