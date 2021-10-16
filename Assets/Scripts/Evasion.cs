using UnityEngine;

public class Evasion : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private float _maxEvasionDistance;

    public Vector3 CenterPosition => _center.position;
    public float MaxEvasionDistance => _maxEvasionDistance;

    public bool AbleToEvade(Vector3 nextPosition)
    {
        if (Vector3.Distance(nextPosition, _center.position) < _maxEvasionDistance)
            return true;

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ParabolicMovement movement))
            Debug.Log("Hit!");
    }
}