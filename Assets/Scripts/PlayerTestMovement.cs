using UnityEngine;

[RequireComponent(typeof(Evasion))]
public class PlayerTestMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _variableJoystick;
    [SerializeField] private Rigidbody _rigidbody;

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

        if (_evasion.AbleToEvade(nextPosition))
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.fixedDeltaTime);
    }
}