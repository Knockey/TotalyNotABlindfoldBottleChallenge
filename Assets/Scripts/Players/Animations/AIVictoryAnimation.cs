public class AIVictoryAnimation : VictoryAnimation
{
    private void OnEnable()
    {
        Watchdog.AIWon += OnAIWon;
    }

    private void OnDisable()
    {
        Watchdog.AIWon -= OnAIWon;
    }

    private void OnAIWon()
    {
        base.Animate();
    }
}
