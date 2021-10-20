using UnityEngine;

[RequireComponent(typeof(Evasion))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _variableJoystick;
    [SerializeField] private Transform _upperTorso;

    private Evasion _evasion;
    private Vector3 _headColliderCenter;
    private Vector3 _upperTorsoCenter;

    private void Awake()
    {
        _evasion = GetComponent<Evasion>();

        _headColliderCenter = transform.position;
        _upperTorsoCenter = _upperTorso.position;
    }

    public void FixedUpdate()
    {
        TryMove();
    }

    private void TryMove()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;
        Vector3 nextPositionOffset = _speed * Time.fixedDeltaTime * direction;

        transform.position = _headColliderCenter + _evasion.GetEvasionDistance(_headColliderCenter, transform.position, nextPositionOffset);
        _upperTorso.position = _upperTorsoCenter + _evasion.GetEvasionDistance(_upperTorsoCenter, _upperTorso.position, nextPositionOffset);
    }
}