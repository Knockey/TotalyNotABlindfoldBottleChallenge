using DG.Tweening;
using UnityEngine;

public class DOTweenSetup : MonoBehaviour
{
    private void Awake()
    {
        DOTween.SetTweensCapacity(1500, 1500);
    }
}
