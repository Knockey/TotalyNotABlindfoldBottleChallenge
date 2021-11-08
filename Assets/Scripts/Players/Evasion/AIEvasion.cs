using System.Collections.Generic;
using UnityEngine;

public class AIEvasion : EvasionMovement
{
    [SerializeField] private List<ParabolicMovementState> _bottles;
    [SerializeField] private float _chanceToEvade;
    [SerializeField] private float _headDirectionAngleDegree;

    private Vector3 _evasionDirection;

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

    protected override void Update()
    {
        TryMove(_evasionDirection);
        base.Update();
    }

    private void OnParabolicMovementStarted(Vector3 startPosition, Vector3 finalPosition)
    {
        if (Random.Range(0, 100) < _chanceToEvade && CheckDirectionTowardsHead(startPosition, finalPosition))
        {
            _evasionDirection = GetEvasionDirection(startPosition, finalPosition); 

            return;
        }

        _evasionDirection = Vector3.zero;
    }

    private static Vector3 GetEvasionDirection(Vector3 startPosition, Vector3 finalPosition)
    {
        Vector3 direction = (finalPosition - startPosition).normalized;
        float xDirection = direction.x;

        direction.x = -1 * direction.z;
        direction.y = 0f;
        direction.z = xDirection;

        return direction;
    }

    private bool CheckDirectionTowardsHead(Vector3 startPosition, Vector3 finalPosition)
    {
        Vector3 toFinalPositionDirection = finalPosition - startPosition;
        Vector3 toHeadDirection = transform.position - startPosition;

        toFinalPositionDirection.y = 0;
        toHeadDirection.y = 0;

        float angle = Vector3.Angle(toFinalPositionDirection, toHeadDirection);

        return angle < _headDirectionAngleDegree || angle > 360f - _headDirectionAngleDegree;
    }
}
