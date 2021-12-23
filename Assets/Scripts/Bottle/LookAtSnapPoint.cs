using UnityEngine;

public class LookAtSnapPoint : MonoBehaviour
{
    [SerializeField] private Transform _snapPoint;

    private void Update()
    {
        transform.LookAt(_snapPoint.position, Vector3.up);
    }
}
