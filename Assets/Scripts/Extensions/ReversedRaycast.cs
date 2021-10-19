using UnityEngine;

public static class ReversedRaycast 
{
    private const float RayLength = 100f;

    public static Vector3 GetRaycastHitPosition(Vector3 origin, Vector3 direction, LayerMask layermask)
    {
        Ray ray = new Ray(origin, direction);
        ray.origin = ray.GetPoint(RayLength);
        ray.direction = -ray.direction;
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layermask);

        return hit.point;
    }
}
