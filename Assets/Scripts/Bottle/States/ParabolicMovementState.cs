using System.Collections.Generic;
using UnityEngine;


public class ParabolicMovementState : State
{
    [SerializeField] private CapsuleCollider _area;
    [SerializeField] private List<Transform> _heads = new List<Transform>();
    [SerializeField] private AnimationCurve _parabolaCurve;
    [SerializeField] private float _chanceToChooseHead;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedModifier;
    [SerializeField] private LayerMask _raycastLayer;

    private readonly List<Vector3> _directions = new List<Vector3>
    {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right
    };
    private Vector3 _direction;
    private Vector3 _startPosition;
    private Vector3 _finalPosition;
    private float _currentSpeed;
    private float _movementTime;

    private void Start()
    {
        _direction = _directions[Random.Range(0, _directions.Count)];

        SetPositions(_direction);
    }

    private void OnEnable()
    {
        _direction = ChooseNewDirection();
        _currentSpeed = _speed;
        _movementTime = 0;

        SetPositions(_direction);
    }

    private void Update()
    {
        MoveTowardsDirection();
    }

    private void FixedUpdate()
    {
        _currentSpeed += _speedModifier;
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

        Ray ray = new Ray(_startPosition, direction);
        ray.origin = ray.GetPoint(100f);
        ray.direction = -ray.direction;

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _raycastLayer);

        _finalPosition = hit.point;
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
        Vector3 newDirection = _area.transform.position;

        return newDirection - transform.position;
    }

    private Vector3 GetNewDirectionFromList()
    {
        return _heads[Random.Range(0, _heads.Count)].position - transform.position;
    }
}