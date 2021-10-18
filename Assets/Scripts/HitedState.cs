using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitedState : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle))
        {
            Vector3 hitDirection = GetHitDirection(other);

            _rigidbody.useGravity = true;
            _rigidbody.AddForce(hitDirection * bottle.HitForce, ForceMode.Impulse);
        }
    }

    private Vector3 GetHitDirection(Collider other)
    {
        return transform.position - other.transform.position;
    }
}
