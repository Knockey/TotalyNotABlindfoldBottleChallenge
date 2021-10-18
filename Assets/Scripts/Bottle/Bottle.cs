using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private float _hitForce;

    public float HitForce => _hitForce;
}
