using UnityEngine;

public class TurnState : State
{
    private const int SpeedModifier = 10;

    [SerializeField] private float _turnSpeed;
    [SerializeField] private Transform _rotationCenter;

    private float _currentDegreesToTurn;
    private void OnEnable()
    {
        float zDifference = _rotationCenter.position.z - transform.position.z;
        _currentDegreesToTurn = zDifference > 0 ? _turnSpeed * SpeedModifier : -(_turnSpeed * SpeedModifier);
    }

    private void Update()
    {
        Turn();
    }

    private void Turn()
    {
        transform.RotateAround(_rotationCenter.position, Vector3.up, _currentDegreesToTurn * Time.deltaTime);
    }
}
