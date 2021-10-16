using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BottleFlyArea : MonoBehaviour
{
    public event Action Leaved;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ParabolicMovement movement))
        {
            Leaved?.Invoke();
        }
    }
}
