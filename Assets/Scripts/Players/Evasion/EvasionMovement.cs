﻿using UnityEngine;

public class EvasionMovement : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialRotation = _spine.rotation;
    }

    protected void TryEvade(Vector3 direction)
    {
        Quaternion evasionQuaternion = GetEvasionQuaternion(direction);
        _spine.rotation = Quaternion.Lerp(_spine.rotation, evasionQuaternion, _rotationSpeed * Time.deltaTime);
    }

    private Quaternion GetEvasionQuaternion(Vector3 direction)
    {
        Vector3 rotationAngle = _initialRotation.eulerAngles + direction * _maxRotationAngle;

        return Quaternion.Euler(rotationAngle);
    }
}