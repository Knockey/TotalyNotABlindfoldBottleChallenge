using UnityEngine;

public class EvasionMovement : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private float _maxEvasionDistance;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _headColliderCenter;
    private Quaternion _initialRotation;
    private Vector3 _currentEvasionDirection;
    private float _currentReturnSpeed;

    private void Awake()
    {
        SetInitialValues();
    }

    protected virtual void Update()
    {
        TryReturnToCenterPosition();
    }

    protected void TryMove(Vector3 direction)
    {
        _currentEvasionDirection = direction;
        Vector3 nextPositionOffset = _speed * Time.deltaTime * direction;

        Vector3 headNextPosition = _headColliderCenter + GetEvasionDistance(_headColliderCenter, transform.position, nextPositionOffset);
        transform.position = Vector3.MoveTowards(transform.position, headNextPosition, _speed * Time.deltaTime);

        Quaternion evasionQuaternion = GetEvasionQuaternion(direction);
        _spine.rotation = Quaternion.Lerp(_spine.rotation, evasionQuaternion, _rotationSpeed * Time.deltaTime);
    }

    private Quaternion GetEvasionQuaternion(Vector3 direction)
    {
        Vector3 rotationAngle = _initialRotation.eulerAngles + direction * _maxRotationAngle;

        return Quaternion.Euler(rotationAngle);
    }

    private Vector3 GetEvasionDistance(Vector3 center, Vector3 currentPosition, Vector3 nextPositionOffset)
    {
        var nextPosition = nextPositionOffset + currentPosition;
        Vector3 offset = nextPosition - center;

        return Vector3.ClampMagnitude(offset, _maxEvasionDistance);
    }

    private void SetInitialValues()
    {
        _headColliderCenter = transform.position;
        _initialRotation = _spine.rotation;
        _currentReturnSpeed = 0f;
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