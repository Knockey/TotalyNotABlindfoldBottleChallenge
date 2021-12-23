using UnityEngine;

public class MovementStartedTransition : Transition
{
    [SerializeField] private TapPanel _tapPanel;

    private void OnEnable()
    {
        _tapPanel.PanelTaped += OnPanelTaped;
    }

    private void OnDisable()
    {
        _tapPanel.PanelTaped -= OnPanelTaped;
    }

    private void OnPanelTaped()
    {
        NeedTransit = true;
    }
}
