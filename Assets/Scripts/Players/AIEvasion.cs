using System.Collections.Generic;
using UnityEngine;

public class AIEvasion : MonoBehaviour
{
    [SerializeField] private List<ParabolicMovementState> _bottles;
    [SerializeField] private float _chanceToEvade;
    [SerializeField] private float _evasionSpeed;
    [SerializeField] private float _headDirectionAngleDegree;

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

    private void OnParabolicMovementStarted(Vector3 startPosition, Vector3 finalPosition)
    {
        if (Random.Range(0, 100) < _chanceToEvade && CheckIsHeadDirection(startPosition, finalPosition))
        {
            Debug.Log($"{gameObject.name}, IT GOES TO ME");
        }
    }

    private bool CheckIsHeadDirection(Vector3 startPosition, Vector3 finalPosition)
    {
        Vector3 toFinalPositionDirection = finalPosition - startPosition;
        Vector3 toHeadDirection = transform.position - startPosition;

        toFinalPositionDirection.y = 0;
        toHeadDirection.y = 0;

        float angle = Vector3.Angle(toFinalPositionDirection, toHeadDirection);

        return angle < _headDirectionAngleDegree || angle > 360f - _headDirectionAngleDegree;
    }
}
