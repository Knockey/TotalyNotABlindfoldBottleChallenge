using UnityEngine;

public class RagdollMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _upperTorsoRigidbory;
    [SerializeField] private HitHandle _hitHandler;

    private void OnEnable()
    {
        _hitHandler.HitPerformed += PushRagdoll;
    }

    private void OnDisable()
    {
        _hitHandler.HitPerformed -= PushRagdoll;
    }

    private void PushRagdoll(Vector3 direction, float hitForce)
    {
        _upperTorsoRigidbory.AddForce(direction * hitForce, ForceMode.Impulse);
    }
}
