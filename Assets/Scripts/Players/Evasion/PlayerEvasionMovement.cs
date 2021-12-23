using UnityEngine;

public class PlayerEvasionMovement : EvasionMovement
{
    [SerializeField] private FloatingJoystick _variableJoystick;
    [SerializeField] private float _maxEvasionDistance;
    [SerializeField] private float _speed;

    private Vector3 _headColliderCenter;
    private Vector3 _currentEvasionDirection;
    private float _currentReturnSpeed;

    private void Awake()
    {
        SetInitialValues();
    }

    private void Update()
    {
        TryGetDirection();
        TryReturnToCenterPosition();
    }

    private void SetInitialValues()
    {
        _headColliderCenter = transform.position;
        _currentReturnSpeed = 0f;
    }

    private void TryGetDirection()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;

        TryMoveCollider(direction);
        TryEvade(direction);
    }

    private void TryMoveCollider(Vector3 direction)
    {
        _currentEvasionDirection = direction;

        Vector3 nextPositionOffset = _speed * Time.deltaTime * direction;
        Vector3 headNextPosition = _headColliderCenter + GetEvasionDistance(_headColliderCenter, transform.position, nextPositionOffset);

        transform.position = Vector3.MoveTowards(transform.position, headNextPosition, _speed * Time.deltaTime);
    }

    private Vector3 GetEvasionDistance(Vector3 center, Vector3 currentPosition, Vector3 nextPositionOffset)
    {
        var nextPosition = nextPositionOffset + currentPosition;
        Vector3 offset = nextPosition - center;

        return Vector3.ClampMagnitude(offset, _maxEvasionDistance);
    }

    private void TryReturnToCenterPosition()
    {
        ChangeReturnSpeedValue();

        transform.position = TryGetReturnPosition(transform.position, _headColliderCenter);
    }

    private void ChangeReturnSpeedValue()
    {
        if (_currentEvasionDirection == Vector3.zero)
        {
            _currentReturnSpeed = _speed;
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