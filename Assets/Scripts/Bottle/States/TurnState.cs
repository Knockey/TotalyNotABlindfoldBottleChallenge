using UnityEngine;

public class TurnState : State
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private CapsuleCollider _area;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 _rotationPoint;
    private int _rotationDirection;

    private void OnEnable()
    {
        _rotationPoint = SetRotationPoint();

        _rotationDirection = _rotationPoint.x > transform.position.x ? 1 : -1;
    }

    private void Update()
    {
        Turn();
    }

    private Vector3 SetRotationPoint()
    {
        Vector3 newRotationPoint = transform.position;
        newRotationPoint.x += Random.Range(0, 1) == 0 ? _offset.x : -_offset.x;
        newRotationPoint.z += Random.Range(0, 1) == 0 ? _offset.z : -_offset.z;

        newRotationPoint = ReversedRaycast.GetRaycastHitPosition(_area.center, newRotationPoint, _layerMask);
        newRotationPoint.y = transform.position.y;

        return newRotationPoint;
    }

    private void Turn()
    {
        transform.RotateAround(_rotationPoint, Vector3.up, _rotationDirection * _turnSpeed * Time.deltaTime);
    }
}
