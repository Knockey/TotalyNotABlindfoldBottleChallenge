using System.Collections.Generic;
using UnityEngine;

public class ParabolicMovementState : State
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<Transform> _ai = new List<Transform>();
    [SerializeField] private float _chanceToChoosePlayer;
    [SerializeField] private float _chanceToChooseAI;
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

    public event System.Action<Vector3, Vector3, ParabolicMovementState> ParabolicMovementStarted;
    public event System.Action<float> RemainDistanceChanged;
    public event System.Action BottleReseted;

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

        BottleReseted?.Invoke();
    }

    private void MoveTowardsDirection()
    {
        float passedDistance = GetNormalizedPassedDistance();
        float normalizedRemainDistance = (_parabolaLength - passedDistance) / _parabolaLength;

        float speed = (_acceleration.Evaluate(normalizedRemainDistance) + _speedModifier);
        RemainDistanceChanged?.Invoke(normalizedRemainDistance);

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _finalPosition + _direction, Time.deltaTime * speed);
        nextPosition.y = _defaultYPosition + _parabolaCurve.Evaluate(normalizedRemainDistance);

        transform.position = nextPosition;
    }

    private void SetPositions(Vector3 direction)
    {
        _startPosition = transform.position;
        _finalPosition = ReversedRaycast.GetRaycastHitPosition(_startPosition, direction, _raycastLayer);
        _parabolaLength = Vector3.Distance(_startPosition, _finalPosition);

        ParabolicMovementStarted?.Invoke(_startPosition, _finalPosition, this);
    }

    private float GetNormalizedPassedDistance()
    {
        return Vector3.Distance(transform.position, _finalPosition);
    }

    private Vector3 ChooseNewDirection()
    {
        if (Random.Range(0, 100) < _chanceToChoosePlayer)
            return GetDirection(GetPlayerDirection);

        if (Random.Range(0, 100) < _chanceToChooseAI)
            return GetDirection(GetNewDirectionFromList);

        return GetDirection(GetNewDirectionFromCapsule);
    }

    private Vector3 GetDirection(System.Func<Vector3> getDirectionMethod)
    {
        Vector3 newDirection = (getDirectionMethod() - transform.position).normalized;
        newDirection.y = 0;

        return newDirection;
    }

    private Vector3 GetPlayerDirection()
    {
        return _player.position;
    }

    private Vector3 GetNewDirectionFromCapsule()
    {
        Vector3 newDirection = -transform.position;
        newDirection.x += Random.Range(-_sphereDirectionOffset.x, _sphereDirectionOffset.x);
        newDirection.z += Random.Range(-_sphereDirectionOffset.z, _sphereDirectionOffset.z);

        return newDirection;
    }

    private Vector3 GetNewDirectionFromList()
    {
        return _ai[Random.Range(0, _ai.Count)].position;
    }
}