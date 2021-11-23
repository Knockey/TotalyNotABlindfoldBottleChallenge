using UnityEngine;

[RequireComponent(typeof(Grayscale))]
public class CameraEffects : MonoBehaviour
{
    [SerializeField] private PlayersConditionWatchdog _watchdog;

    private Grayscale _grayscaleEffect;

    private void Awake()
    {
        _grayscaleEffect = GetComponent<Grayscale>();
        _grayscaleEffect.enabled = false;
    }

    private void OnEnable()
    {
        _watchdog.AIWon += OnWon;
        _watchdog.PlayerWon += OnWon;
    }

    private void OnDisable()
    {
        _watchdog.AIWon -= OnWon;
        _watchdog.PlayerWon -= OnWon;
    }

    private void OnWon()
    {
        _grayscaleEffect.enabled = true;
    }
}
