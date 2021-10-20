using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandle : MonoBehaviour
{
    [SerializeField] private HitDetection _detection;
    [SerializeField] private GameObject _ragdoll;

    public event Action<Vector3, float> HitPerformed;

    private void OnEnable()
    {
        _detection.ColliderHited += OnColliderHited;
    }

    private void OnDisable()
    {
        _detection.ColliderHited -= OnColliderHited;
    }

    private void OnColliderHited(Vector3 direction, float hitForce, GameObject sender)
    {
        sender.SetActive(false);
        _ragdoll.SetActive(true);

        HitPerformed?.Invoke(direction, hitForce);
    }
}
