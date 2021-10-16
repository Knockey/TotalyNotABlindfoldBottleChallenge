using System.Collections.Generic;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
    [SerializeField] private BottleFlyArea _area;
    [SerializeField] private List<Transform> _heads = new List<Transform>();
    [SerializeField] private float _chanceToChooseHead;
    [SerializeField] private float _speed;
    [SerializeField] private float _reboundAngleOffset;

    private Vector3 _direction;

    private void OnEnable()
    {
        _area.Leaved += ChooseNewDirection;
    }
    private void OnDisable()
    {
        _area.Leaved -= ChooseNewDirection;
    }

    private void Start()
    {
        _direction = Vector3.forward;
    }

    private void Update()
    {
        MoveTowardsDirection();
    }

    private void MoveTowardsDirection()
    {
        transform.Translate(_speed * Time.deltaTime * _direction);
    }

    private void ChooseNewDirection()
    {
        if (Random.Range(0, 100) < _chanceToChooseHead)
        {
            _direction = GetNewDirectionFromList().normalized;
            _direction.y = 0;
            return;
        }

        _direction = GetNewDirectionFromCapsule().normalized;
        _direction.y = 0;
    }

    private Vector3 GetNewDirectionFromCapsule()
    {
        var capsule = _area.GetComponent<CapsuleCollider>();
        Vector3 newDirection = capsule.transform.position;

        return newDirection - transform.position;
    }

    private Vector3 GetNewDirectionFromList()
    {
        return _heads[Random.Range(0, _heads.Count)].position - transform.position;
    }
}