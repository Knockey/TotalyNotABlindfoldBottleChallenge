using System;
using System.Collections.Generic;
using UnityEngine;

public class EvasionMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _spineParts = new List<Transform>();
    [SerializeField] private Transform _hips;
    [SerializeField] private float _maxEvasionDistance;
    [SerializeField] private AnimationCurve _spineBend;
    [SerializeField] private float _speed;
    [SerializeField] private float _returnSpeed;

    private List<Vector3> _centerPositions = new List<Vector3>();
    private Vector3 _headColliderCenter;
    private Vector3 _currentEvasionDirection;
    private float _currentReturnSpeed;

    private void Awake()
    {
        GetCenterPositions();

        _currentReturnSpeed = 0f;
    }

    protected virtual void Update()
    {
        TryReturnToCenterPosition();
    }

    protected void TryMove(Vector3 direction)
    {
        _currentEvasionDirection = direction;
        Vector3 nextPositionOffset = _speed * Time.deltaTime * direction;

        Vector3 headNextPosition = _headColliderCenter + GetEvasionDistance(_headColliderCenter, transform.position, nextPositionOffset, 1f);
        transform.position = Vector3.MoveTowards(transform.position, headNextPosition, _speed * Time.deltaTime);

        for (int i = 0; i < _spineParts.Count; i++)
        {
            float spineBendArgument = GetSpineBendArgument(_spineParts[i].position);
            Vector3 spinePartEvasionDistance = GetEvasionDistance(_centerPositions[i], _spineParts[i].position, nextPositionOffset, _spineBend.Evaluate(spineBendArgument));
            Vector3 spinePartNextPosition = _centerPositions[i] + spinePartEvasionDistance;
            _spineParts[i].position = Vector3.MoveTowards(_spineParts[i].position, spinePartNextPosition, _speed * Time.deltaTime);
        }
    }

    private float GetSpineBendArgument(Vector3 spinePart)
    {
        float distanceToBodyPart = spinePart.y - _hips.position.y;
        float distanceToHead = transform.position.y - _hips.position.y;

        return distanceToBodyPart / distanceToHead;
    }

    private Vector3 GetEvasionDistance(Vector3 center, Vector3 currentPosition, Vector3 nextPositionOffset, float spineBendModifier)
    {
        var nextPosition = nextPositionOffset + currentPosition;
        Vector3 offset = nextPosition - center;

        return Vector3.ClampMagnitude(offset, _maxEvasionDistance * spineBendModifier);
    }

    private void GetCenterPositions()
    {
        _headColliderCenter = transform.position;

        foreach (var spinePart in _spineParts)
        {
            _centerPositions.Add(spinePart.position);
        }
    }

    private void TryReturnToCenterPosition()
    {
        SetReturnSpeed();

        transform.position = TryGetReturnPosition(transform.position, _headColliderCenter);

        for (int i = 0; i < _spineParts.Count; i++)
        {
            _spineParts[i].position = TryGetReturnPosition(_spineParts[i].position, _centerPositions[i]);
        }
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

    private Vector3 TryGetReturnPosition(Vector3 current, Vector3 center)
    {
        if (current != center)
            return Vector3.MoveTowards(current, center, _currentReturnSpeed * Time.deltaTime);

        return current;
    }
}