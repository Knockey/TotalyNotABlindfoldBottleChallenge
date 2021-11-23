using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class BottleApproachHandle : MonoBehaviour
{
    private Bottle _bottle;
    private AudioSource _approachingBottleSound;

    public event Action<float> BottleApproaching;
    public event Action BottleFlewAway;

    private void Awake()
    {
        _approachingBottleSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bottle bottle) && _bottle == null)
        {
            _bottle = bottle;
            _approachingBottleSound.Stop();
            _approachingBottleSound.Play();
        }
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
