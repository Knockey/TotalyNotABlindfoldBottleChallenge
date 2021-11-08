using UnityEngine;

public class PlayerEvasionMovement : EvasionMovement
{
    [SerializeField] private FloatingJoystick _variableJoystick;

    protected override void Update()
    {
        TryGetDirection();
        base.Update();
    }

    private void TryGetDirection()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;

        TryMove(direction);
    }
}