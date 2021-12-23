using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _initialState;

    protected State CurrentState;

    public State Current => CurrentState;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        CurrentState = _initialState;
        CurrentState.Enter();
    }

    private void Update()
    {
        if (CurrentState == null)
            return;

        var nextState = CurrentState.GetNext();

        if (nextState != null)
            Transit(nextState);
    }

    protected virtual void Transit(State nextState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = nextState;
        CurrentState.Enter();
    }
}
