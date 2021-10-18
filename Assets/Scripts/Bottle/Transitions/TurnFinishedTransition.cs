using System.Collections;
using UnityEngine;

public class TurnFinishedTransition : Transition
{
    [SerializeField] private float _turnTime;

    private void OnEnable()
    {
        StartCoroutine(TurnTimer());
        NeedTransit = false;
    }

    private IEnumerator TurnTimer()
    {
        yield return new WaitForSeconds(_turnTime);

        NeedTransit = true;
    }
}
