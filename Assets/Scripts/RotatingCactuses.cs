using UnityEngine;

public class RotatingCactuses : MonoBehaviour
{
    [SerializeField] private float _rotationAngle;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, _rotationAngle, 0f));
    }
}
