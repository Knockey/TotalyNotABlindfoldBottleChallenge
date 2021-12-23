using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BottleApproachHandle : MonoBehaviour
{
    private Bottle _bottle;

    public event Action<float> BottleApproaching;
    public event Action BottleFlewAway;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle) && _bottle == null)
            _bottle = bottle;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle) && bottle == _bottle)
        {
            float distanceToBottle = Vector3.Distance(transform.position, bottle.transform.position);
            BottleApproaching?.Invoke(distanceToBottle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle) && _bottle == bottle)
        {
            _bottle = null;
            BottleFlewAway?.Invoke();
        }
    }
}
