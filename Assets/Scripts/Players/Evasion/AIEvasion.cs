using System.Collections.Generic;
using UnityEngine;

public class AIEvasion : EvasionMovement
{
    private const float EvasionAngle = 90f;

    [SerializeField] private List<ParabolicMovementState> _bottles;
    [SerializeField] private float _chanceToEvade;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 _evasionDirection;
    private bool _isNotEvading = true;
    private ParabolicMovementState _currentEvasionBottle;

    private void OnEnable()
    {
        foreach (var bottle in _bottles)
        {
            bottle.ParabolicMovementStarted += OnParabolicMovementStarted;
        }
    }

    private void OnDisable()
    {
        foreach (var bottle in _bottles)
        {
            bottle.ParabolicMovementStarted -= OnParabolicMovementStarted;
        }
    }

    private void Update()
    {
        TryEvade(_evasionDirection);
    }

    private void OnParabolicMovementStarted(Vector3 startPosition, Vector3 finalPosition, ParabolicMovementState bottle)
    {
        TryResetEvasionState(bottle);

        if (Random.Range(0, 100) < _chanceToEvade && _isNotEvading)
            SetEvasionDirection(startPosition, finalPosition, bottle);
    }

    private void TryResetEvasionState(ParabolicMovementState bottle)
    {
        if (bottle == _currentEvasionBottle || _currentEvasionBottle == null)
        {
            _isNotEvading = true;
            _currentEvasionBottle = null;
        }
    }

    private void SetEvasionDirection(Vector3 startPosition, Vector3 finalPosition, ParabolicMovementState bottle)
    {
        if (IsDirectedTowardsHead(startPosition, finalPosition))
        {
            _evasionDirection = GetEvasionDirection(startPosition, finalPosition);
            _isNotEvading = false;
            _currentEvasionBottle = bottle;

            return;
        }

        _evasionDirection = Vector3.zero;
    }

    private static Vector3 GetEvasionDirection(Vector3 startPosition, Vector3 finalPosition)
    {
        Vector3 direction = (finalPosition - startPosition).normalized;
        Quaternion evasionQuaternion = new Quaternion(Vector3.up.x, Vector3.up.y, Vector3.up.z, EvasionAngle);

        return (evasionQuaternion * direction).normalized;
    }

    private bool IsDirectedTowardsHead(Vector3 startPosition, Vector3 finalPosition)
    {
        Vector3 toFinalPositionDirection = finalPosition - startPosition;

        startPosition.y = transform.position.y;

        ReversedRaycast.GetRaycastHitPosition(startPosition, toFinalPositionDirection, out RaycastHit hitObj, _layerMask);

        return hitObj.collider != null && hitObj.collider.gameObject == gameObject;
    }
}
