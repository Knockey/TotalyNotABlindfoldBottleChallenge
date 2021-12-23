using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ShatteredState : State
{
    [SerializeField] private AudioSource _shatterSound;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float _beforeDestroyTime;
    [SerializeField] private ArrowRender _arrow;
    [SerializeField] private Rigidbody _wrapper;
    [SerializeField] private LineRenderer _rope;

    private MeshRenderer _mesh;

    private void OnEnable()
    {
        _mesh = GetComponent<MeshRenderer>();

        DestroyBottle();
    }

    private void DestroyBottle()
    {
        _shatterSound.Play();

        _rope.gameObject.SetActive(false);
        _wrapper.useGravity = true;
        _mesh.enabled = false;
        _particles.gameObject.SetActive(true);
        _arrow.gameObject.SetActive(false);

        Destroy(gameObject, _beforeDestroyTime);
    }
}
