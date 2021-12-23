using UnityEngine;

public class MovementFinishedTransition : Transition
{
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BottleFlyArea area))
            NeedTransit = true;
    }
}
