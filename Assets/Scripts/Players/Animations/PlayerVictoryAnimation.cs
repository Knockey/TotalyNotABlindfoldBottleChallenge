using UnityEngine;

public class PlayerVictoryAnimation : VictoryAnimation
{
    private void OnEnable()
    {
        Watchdog.PlayerWon += OnPlayerWon;
    }

    private void OnDisable()
    {
        Watchdog.PlayerWon -= OnPlayerWon;
    }

    private void OnPlayerWon()
    {
        base.Animate();
    }
}
