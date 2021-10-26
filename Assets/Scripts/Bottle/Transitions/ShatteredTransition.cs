using UnityEngine;

public class ShatteredTransition : Transition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EvasionMovement movement))
            NeedTransit = true;
    }
}
