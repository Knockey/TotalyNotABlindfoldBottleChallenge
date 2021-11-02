using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParabolicMovementState : State
{
    [SerializeField] private List<Transform> _heads = new List<Transform>();
    [SerializeField] private float _chanceToChooseHead;
    [SerializeField] private AnimationCurve _parabolaCurve;
    [SerializeField] private AnimationCurve _acceleration;
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
    private float _parabolaLength;
    private float _defaultYPosition;

    public event UnityAction<Vector3, Vector3> ParabolicMovementStarted;

    private void Awake()
    {
        _defaultYPosition = transform.position.y;
    }

    private void OnEnable()
    {
        ResetBottle();
    }

    private void Start()
    {
        _direction = _directions[Random.Range(0, _directions.Count)];

        SetPositions(_direction);
    }

    private void Update()
    {
        MoveTowardsDirection();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BottleFlyArea area))
            ResetBottle();
    }

    private void ResetBottle()
    {
        _direction = ChooseNewDirection();

        SetPositions(_direction);
    }

    private void MoveTowardsDirection()
    {
        float passedDistance = GetNormalizedPassedDistance();
        float normalizedRemainDistance = (_parabolaLength - passedDistance) / _parabolaLength;

        float speed = (_acceleration.Evaluate(normalizedRemainDistance) + _speedModifier);
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _finalPosition + _direction, Time.deltaTime * speed);
        nextPosition.y = _defaultYPosition + _parabolaCurve.Evaluate(normalizedRemainDistance);

        transform.position = nextPosition;
    }

    private void SetPositions(Vector3 direction)
    {
        _startPosition = transform.position;
        _finalPosition = ReversedRaycast.GetRaycastHitPosition(_startPosition, direction, _raycastLayer);
        _parabolaLength = Vector3.Distance(_startPosition, _finalPosition);

        ParabolicMovementStarted?.Invoke(_startPosition, _finalPosition);
    }

    private float GetNormalizedPassedDistance()
    {
        return Vector3.Distance(transform.position, _finalPosition);
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