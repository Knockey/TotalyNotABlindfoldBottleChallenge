using UnityEngine;

public class ArrowRender : MonoBehaviour
{
    [SerializeField] private LayerMask _floorLayerMask;
    [SerializeField] private ParabolicMovementState _bottle;

    private void OnEnable()
    {
        _bottle.ParabolicMovementStarted += OnParabolicMovementStarted;
    }

    private void OnDisable()
    {
        _bottle.ParabolicMovementStarted -= OnParabolicMovementStarted;
    }

    private void Update()
    {
        RenderArrowAndShadow();
    }

    private void OnParabolicMovementStarted(Vector3 bottleStartPosition, Vector3 bottleFinishPosition, ParabolicMovementState bottle)
    {
        bottleFinishPosition.y = transform.position.y;
        transform.LookAt(bottleFinishPosition);
    }

    private void RenderArrowAndShadow()
    {
        transform.position = GetArrowPosition();
    }

    private Vector3 GetArrowPosition()
    {
        if (Physics.Raycast(_bottle.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, _floorLayerMask))
        {
            Vector3 newArrowPosition = hit.point;
            newArrowPosition.y += 0.05f;

            return newArrowPosition;
        }

        return Vector3.zero;
    }
}
