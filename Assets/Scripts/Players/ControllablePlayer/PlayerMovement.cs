using UnityEngine;

[RequireComponent(typeof(Evasion))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _variableJoystick;

    private Evasion _evasion;

    private void Awake()
    {
        _evasion = GetComponent<Evasion>();
    }

    public void FixedUpdate()
    {
        TryMove();
    }

    private void TryMove()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;
        Vector3 nextPosition = _speed * Time.fixedDeltaTime * direction + transform.position;

        Vector3 offset = nextPosition - _evasion.CenterPosition;
        transform.position = _evasion.CenterPosition + Vector3.ClampMagnitude(offset, _evasion.MaxEvasionDistance);
    }
}