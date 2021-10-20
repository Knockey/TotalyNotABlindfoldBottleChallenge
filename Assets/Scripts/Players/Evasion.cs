using UnityEngine;

public class Evasion : MonoBehaviour
{
    [SerializeField] private float _maxEvasionDistance;

    public float MaxEvasionDistance => _maxEvasionDistance;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ParabolicMovementState movement))
            Debug.Log("Hit!");
    }
    public bool CanEvade(Vector3 center, Vector3 nextPosition)
    {
        if (Vector3.Distance(center, nextPosition) < _maxEvasionDistance)
            return true;

        return false;
    }

    public Vector3 GetEvasionDistance(Vector3 center, Vector3 currentPosition, Vector3 nextPositionOffset)
    {
        var nextPosition = nextPositionOffset + currentPosition;
        Vector3 offset = nextPosition - center;

        return Vector3.ClampMagnitude(offset, _maxEvasionDistance);
    }
}