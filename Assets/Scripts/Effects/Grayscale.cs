using UnityEngine;

public class Grayscale : MonoBehaviour
{
    [SerializeField] private Shader _grayscaleShader;

    private Material _grayscaleMaterial;

    private void Awake()
    {
        _grayscaleMaterial = new Material(_grayscaleShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _grayscaleMaterial);
    }
}
