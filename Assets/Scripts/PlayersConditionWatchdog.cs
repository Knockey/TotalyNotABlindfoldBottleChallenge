using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayersConditionWatchdog : MonoBehaviour
{
    [SerializeField] private List<HitDetection> _players = new List<HitDetection>();
    
    private int _countOfAI;
    private int _deadAICount;

    public event Action PlayedWon;
    public event Action AIWon;

    private void OnEnable()
    {
        _deadAICount = 0;
        _countOfAI = 0;

        GetAICount();

        foreach (var player in _players)
        {
            player.ColliderHitedWithType += OnCollisionHitWithType;
        }
    }

    private void OnDisable()
    {
        foreach (var player in _players)
        {
            player.ColliderHitedWithType -= OnCollisionHitWithType;
        }
    }

    private void OnCollisionHitWithType(EvasionMovement movementType)
    {
        switch (movementType)
        {
            case AIEvasion movement:
                CheckAliveAI();
                break;

            case PlayerEvasionMovement movement:
                AIWon?.Invoke();
                break;
        }
    }

    private void CheckAliveAI()
    {
        _deadAICount++;

        if (_deadAICount == _countOfAI)
            PlayedWon?.Invoke();
    }

    private void GetAICount()
    {
        foreach (var player in _players)
        {
            if (player.TryGetComponent(out AIEvasion evasion))
                _countOfAI++;
        }
    }
}
