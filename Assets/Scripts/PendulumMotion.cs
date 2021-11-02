using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMotion : MonoBehaviour
{
    private const float SelfDeltaTime = 0.01f;

    [SerializeField] private Transform _ropeSnapPoint;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _ropeLength = 2f;

    private Vector3 _startPosition;

    // You could define these in the `PendulumUpdate()` loop 
    // But we want them in the class scope so we can draw gizmos `OnDrawGizmos()`
    private Vector3 _gravityDirection;
    private Vector3 _tensionDirection;
    private Vector3 _tangentDirection;
    private Vector3 _pendulumSideDirection;
    private float _tensionForce = 0f;
    private float _gravityForce = 0f;


    // Keep track of the current velocity
    private Vector3 _currentVelocity;

    // We use these to smooth between values in certain framerate situations in the `Update()` loop
    Vector3 _currentStatePosition;
    Vector3 _previousStatePosition;

    private float _accumulator = 0f;

    // Use this for initialization
    private void Start()
    {
        // Set the starting position for later use in the context menu reset methods
        _startPosition = transform.position;
        ResetPendulumForces();
    }


    private void Update()
    {
        MoveWithFixedTimeStep();
    }

    private void MoveWithFixedTimeStep()
    {
        _accumulator += Time.deltaTime;

        while (_accumulator >= SelfDeltaTime)
        {
            _previousStatePosition = _currentStatePosition;
            _currentStatePosition = PendulumUpdate(_currentStatePosition, SelfDeltaTime);
            _accumulator -= SelfDeltaTime;
        }

        float alpha = _accumulator / SelfDeltaTime;

        Vector3 newPosition = _currentStatePosition * alpha + _previousStatePosition * (1f - alpha);

        transform.position = newPosition;
    }

    // Use this to reset any built up forces
    private void ResetPendulumForces()
    {
        _currentVelocity = Vector3.zero;

        // Set the transition state
        _currentStatePosition = transform.position;
    }


    private Vector3 PendulumUpdate(Vector3 currentStatePosition, float deltaTime)
    {
        // Add gravity free fall
        _gravityForce = _mass * Physics.gravity.magnitude;
        _gravityDirection = Physics.gravity.normalized;
        _currentVelocity += _gravityDirection * _gravityForce * deltaTime;


        Vector3 auxiliaryMovementDelta = _currentVelocity * deltaTime;
        float distanceAfterGravity = Vector3.Distance(_ropeSnapPoint.position, _currentStatePosition + auxiliaryMovementDelta);

        //If at the end of the rope
        if (distanceAfterGravity > _ropeLength || Mathf.Approximately(distanceAfterGravity, _ropeLength))
        {

            _tensionDirection = (_ropeSnapPoint.position - _currentStatePosition).normalized;

            _pendulumSideDirection = (Quaternion.Euler(0f, 90f, 0f) * _tensionDirection);
            _pendulumSideDirection.Scale(new Vector3(1f, 0f, 1f));
            _pendulumSideDirection.Normalize();

            _pendulumSideDirection = Vector3.left;

            _tangentDirection = (-1f * Vector3.Cross(_tensionDirection, _pendulumSideDirection)).normalized;


            float inclinationAngle = Vector3.Angle(_currentStatePosition - _ropeSnapPoint.position, _gravityDirection);

            _tensionForce = _mass * Physics.gravity.magnitude * Mathf.Cos(Mathf.Deg2Rad * inclinationAngle);
            float centripetalForce = ((_mass * Mathf.Pow(_currentVelocity.magnitude, 2)) / _ropeLength);
            _tensionForce += centripetalForce;

            _currentVelocity += _tensionDirection * _tensionForce * deltaTime;

            Debug.Log(_pendulumSideDirection);
        }

        // Get the movement delta
        Vector3 movementDelta = Vector3.zero;
        movementDelta += _currentVelocity * deltaTime;

        float distance = Vector3.Distance(_ropeSnapPoint.position, currentStatePosition + movementDelta);
        return GetPointOnLine(_ropeSnapPoint.position, currentStatePosition + movementDelta, distance <= _ropeLength ? distance : _ropeLength);
    }

    private Vector3 GetPointOnLine(Vector3 start, Vector3 end, float distanceFromStart)
    {
        return start + (distanceFromStart * Vector3.Normalize(end - start));
    }





    //Gizmos for check and nothing else;
    void OnDrawGizmos()
    {
        // purple
        Gizmos.color = new Color(.5f, 0f, .5f);
        Gizmos.DrawWireSphere(_ropeSnapPoint.position, _ropeLength);

        Gizmos.DrawWireCube(_startPosition, new Vector3(.5f, .5f, .5f));


        // Blue: Auxilary
        Gizmos.color = new Color(.3f, .3f, 1f); // blue
        Vector3 auxVel = .3f * _currentVelocity;
        Gizmos.DrawRay(transform.position, auxVel);
        Gizmos.DrawSphere(transform.position + auxVel, .2f);

        // Yellow: Gravity
        Gizmos.color = new Color(1f, 1f, .2f);
        Vector3 gravity = .3f * _gravityForce * _gravityDirection;
        Gizmos.DrawRay(transform.position, gravity);
        Gizmos.DrawSphere(transform.position + gravity, .2f);

        // Orange: Tension
        Gizmos.color = new Color(1f, .5f, .2f); // Orange
        Vector3 tension = .3f * _tensionForce * _tensionDirection;
        Gizmos.DrawRay(transform.position, tension);
        Gizmos.DrawSphere(transform.position + tension, .2f);

        // Red: Resultant
        Gizmos.color = new Color(1f, .3f, .3f); // red
        Vector3 resultant = gravity + tension;
        Gizmos.DrawRay(transform.position, resultant);
        Gizmos.DrawSphere(transform.position + resultant, .2f);



        // Green: Pendulum side direction
        Gizmos.color = new Color(.3f, 1f, .3f);
        Gizmos.DrawRay(transform.position, 3f * _pendulumSideDirection);
        Gizmos.DrawSphere(transform.position + 3f * _pendulumSideDirection, .2f);



        // Cyan: tangent direction
        Gizmos.color = new Color(.2f, 1f, 1f); // cyan
        Gizmos.DrawRay(transform.position, 3f * _tangentDirection);
        Gizmos.DrawSphere(transform.position + 3f * _tangentDirection, .2f);
    }
}
