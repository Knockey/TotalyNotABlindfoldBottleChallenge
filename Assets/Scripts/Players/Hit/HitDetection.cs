using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(EvasionMovement))]
public class HitDetection : MonoBehaviour
{
    private EvasionMovement _movement;
    private Collider _hitCollider;

    public event Action<Vector3, float, GameObject> ColliderHitedInDirection;
    public event Action<EvasionMovement> ColliderHitedWithType;

    private void Awake()
    {
        _hitCollider = GetComponent<Collider>();
        _movement = GetComponent<EvasionMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle))
        {
            _hitCollider.enabled = false;

            Vector3 direction = transform.position - bottle.transform.position;

            ColliderHitedInDirection?.Invoke(direction, bottle.HitForce, transform.parent.gameObject);
            ColliderHitedWithType?.Invoke(_movement);
        }
    }
}
