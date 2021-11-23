using UnityEngine;

public class EvasionMovement : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialRotation = _spine.localRotation;
    }

    protected void TryEvade(Vector3 direction)
    {
        Quaternion evasionQuaternion = GetEvasionQuaternion(direction);
        _spine.localRotation = Quaternion.Lerp(_spine.localRotation, evasionQuaternion, _rotationSpeed * Time.deltaTime);
    }

    private Quaternion GetEvasionQuaternion(Vector3 direction)
    {
        Vector3 rotationAngle = _initialRotation.eulerAngles + direction * _maxRotationAngle;

        return Quaternion.Euler(rotationAngle);
    }
}