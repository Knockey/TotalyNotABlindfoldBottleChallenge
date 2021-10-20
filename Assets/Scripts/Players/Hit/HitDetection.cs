using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HitDetection : MonoBehaviour
{
    private Collider _hitCollider;

    public event Action<Vector3, float,  GameObject> ColliderHited;

    private void Awake()
    {
        _hitCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle))
        {
            _hitCollider.enabled = false;

            Vector3 direction = transform.position - bottle.transform.position;

            ColliderHited?.Invoke(direction, bottle.HitForce, transform.parent.gameObject);
        }
    }
}
