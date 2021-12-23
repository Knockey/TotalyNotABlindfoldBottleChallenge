using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeRenderer : MonoBehaviour
{
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private Transform _ropeConnector;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, _ropeConnector.position);
        _lineRenderer.SetPosition(1, _snapPoint.position);
    }
}
