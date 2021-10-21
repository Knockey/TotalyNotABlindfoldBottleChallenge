using UnityEngine;

public class PlayerEvasionMovement : EvasionMovement
{
    [SerializeField] private FloatingJoystick _variableJoystick;

    private void FixedUpdate()
    {
        TryGetDirection();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ParabolicMovementState bottle))
            InvokeCollidedEvent(this);
    }

    private void TryGetDirection()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;

        TryMove(direction);
    }
}