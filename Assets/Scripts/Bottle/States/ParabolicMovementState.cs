using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParabolicMovementState : State
{
    [SerializeField] private List<Transform> _heads = new List<Transform>();
    [SerializeField] private AnimationCurve _parabolaCurve;
    [SerializeField] private float _chanceToChooseHead;
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _speedModifier;
    [SerializeField] private Vector3 _sphereDirectionOffset;
    [SerializeField] private LayerMask _raycastLayer;

    private readonly List<Vector3> _directions = new List<Vector3>
    {
        Vector3.forward + Vector3.left,
        Vector3.back + Vector3.left,
        Vector3.forward + Vector3.right,
        Vector3.back + Vector3.right
    };
    private Vector3 _direction;
    private Vector3 _startPosition;
    private Vector3 _finalPosition;
    private float _currentSpeed;
    private float _movementTime;

    public event UnityAction<Vector3, Vector3> ParabolicMovementStarted;

    private void Start()
    {
        _direction = _directions[Random.Range(0, _directions.Count)];

        SetPositions(_direction);
    }

    private void OnEnable()
    {
        _direction = ChooseNewDirection();
        _currentSpeed = _initialSpeed;
        _movementTime = 0f;

        SetPositions(_direction);
    }

    private void Update()
    {
        MoveTowardsDirection();

        _currentSpeed += _speedModifier * Time.deltaTime;
    }

    private void MoveTowardsDirection()
    {
        _movementTime += Time.deltaTime;
        Vector3 nextPosition = Vector3.Lerp(_startPosition, _finalPosition + _direction, _movementTime * _currentSpeed);
        nextPosition.y += _parabolaCurve.Evaluate(_movementTime * _currentSpeed);

        transform.position = nextPosition;
    }

    private void SetPositions(Vector3 direction)
    {
        _startPosition = transform.position;
        _finalPosition = ReversedRaycast.GetRaycastHitPosition(_startPosition, direction, _raycastLayer);

        ParabolicMovementStarted?.Invoke(_startPosition, _finalPosition);
    }

    private Vector3 ChooseNewDirection()
    {
        if (Random.Range(0, 100) < _chanceToChooseHead)
        {
            return GetDirection(GetNewDirectionFromList);
        }

        return GetDirection(GetNewDirectionFromCapsule);
    }

    private Vector3 GetDirection(System.Func<Vector3> getDirectionMethod)
    {
        Vector3 newDirection = getDirectionMethod().normalized;
        newDirection.y = 0;

        return newDirection;
    }

    private Vector3 GetNewDirectionFromCapsule()
    {
        Vector3 newDirection = -transform.position;
        newDirection.x += Random.Range(-_sphereDirectionOffset.x, _sphereDirectionOffset.x);
        newDirection.z += Random.Range(-_sphereDirectionOffset.z, _sphereDirectionOffset.z);

        return newDirection - transform.position;
    }

    private Vector3 GetNewDirectionFromList()
    {
        return _heads[Random.Range(0, _heads.Count)].position - transform.position;
    }
}