using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _variableJoystick;
    [SerializeField] private Rigidbody _rigidbody;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;

        _rigidbody.AddForce(direction * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}