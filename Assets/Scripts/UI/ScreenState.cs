using UnityEngine;

public class ScreenState : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedPanel;
    [SerializeField] private GameObject _levelFailedPanel;
    [SerializeField] private PlayersConditionWatchdog _watchdog;

    private bool _isFailPanelOn;
    private bool _isWonPanelOn;

    private void OnEnable()
    {
        _isFailPanelOn = false;
        _isWonPanelOn = false;

        _watchdog.AIWon += () => TurnOnPanel(_levelFailedPanel, ref _isFailPanelOn, _isWonPanelOn);
        _watchdog.PlayerWon += () => TurnOnPanel(_levelCompletedPanel, ref _isWonPanelOn, _isFailPanelOn); 
    }

    private void OnDisable()
    {
        _watchdog.AIWon -= () => TurnOnPanel(_levelFailedPanel, ref _isFailPanelOn, _isWonPanelOn);
        _watchdog.PlayerWon -= () => TurnOnPanel(_levelCompletedPanel, ref _isWonPanelOn, _isFailPanelOn);
    }

    private void TurnOnPanel(GameObject panel,ref bool panelTurnedOnState, bool otherPanelTurnedOnState)
    {
        if (otherPanelTurnedOnState == false)
        {
            panel.SetActive(true);
            panelTurnedOnState = true;
        }
    }
}
