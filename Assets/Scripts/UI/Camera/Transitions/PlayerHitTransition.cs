using UnityEngine;

public class PlayerHitTransition : Transition
{
    [SerializeField] private HitDetection _hitDetection;

    private void OnEnable()
    {
        _hitDetection.ColliderHitedWithType += OnColliderHitedWithType;
    }

    private void OnDisable()
    {
        _hitDetection.ColliderHitedWithType -= OnColliderHitedWithType;
    }

    private void OnColliderHitedWithType(EvasionMovement type)
    {
        if (type.TryGetComponent(out PlayerEvasionMovement player))
        {
            NeedTransit = true;
        }
    }
}
