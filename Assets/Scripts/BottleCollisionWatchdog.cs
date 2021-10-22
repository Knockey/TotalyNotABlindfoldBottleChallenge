using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollisionWatchdog : MonoBehaviour
{
    [SerializeField] private List<HitDetection> _players = new List<HitDetection>();

    private void OnEnable()
    {
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
                Debug.Log(movement.GetType());
                break;

            case PlayerEvasionMovement movement:
                Debug.Log(movement.GetType());
                break;
        }
    }
}
