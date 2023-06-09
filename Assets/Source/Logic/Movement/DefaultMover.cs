using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefaultMover : MonoBehaviour, IMover
{
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    
    [SerializeField] private bool alignMovementWithCamera;
    [SerializeField] private Transform cameraTopTransform;
    
    [SerializeField] private float targetHorizontalSpeed = 15;
    [SerializeField] private float groundHorizontalAcceleration = 75;
    [SerializeField] private float airHorizontalAcceleration = 25;
    
    private CharacterController _ch;

    private Vector2 _horizontalInput;
    private Vector3 _velocity;

    private bool _grounded = true;
    private bool _isResponseToInput = true;
    
    public event Action OnLanding;

    private void Awake()
    {
        _ch = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var newGroundedState = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        
        if (newGroundedState && !_grounded)
            OnLanding?.Invoke();
        _grounded = newGroundedState;
        
        PerformMove();
    }


    public void PerformMove()
    {
        if (_isResponseToInput)
        {
            var horizontalInputDirection = new Vector3(_horizontalInput.x, 0, _horizontalInput.y).normalized;

            if (alignMovementWithCamera)
            {
                horizontalInputDirection = horizontalInputDirection.x * cameraTopTransform.right +
                                           horizontalInputDirection.z * cameraTopTransform.forward;
            }

            var acceleration = IsGrounded() ? groundHorizontalAcceleration : airHorizontalAcceleration;
            if (!horizontalInputDirection.x.Equals(0) && !Mathf.Sign(horizontalInputDirection.x).Equals(Mathf.Sign(_velocity.x)) ||
                !horizontalInputDirection.z.Equals(0) && !Mathf.Sign(horizontalInputDirection.z).Equals(Mathf.Sign(_velocity.z)))
                acceleration *= 5;
            acceleration *= Time.deltaTime;

            var desiredVelocity = horizontalInputDirection * targetHorizontalSpeed;

            var horizontalVelocity = Vector3.MoveTowards(_velocity, desiredVelocity, acceleration);

            _velocity.x = horizontalVelocity.x;
            _velocity.z = horizontalVelocity.z;
        }
        if (GetHorizontalSpeed() > 0.1f)
            transform.rotation = Quaternion.LookRotation(new Vector3(_velocity.x, 0, _velocity.z));
        _ch.Move(_velocity * Time.deltaTime);
    }

    public void SetHorizontalInput(Vector2 input)
    {
        _horizontalInput = input;
    }

    public void SetVerticalVelocity(float velocity)
    {
        _velocity.y = velocity;
    }

    public void SetMaxSpeed(float value)
    {
        targetHorizontalSpeed = value;
    }

    public void AddVerticalVelocity(float addedVelocity)
    {
        _velocity.y += addedVelocity;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        _velocity = newVelocity;
    }

    public void SetHorizontalVelocity(Vector3 newVelocity)
    {
        _velocity.x = newVelocity.x;
        _velocity.z = newVelocity.z;
    }

    public void AddVelocity(Vector3 addedVelocityVector)
    {
        _velocity += addedVelocityVector;
    }

    public void SetInputResponse(bool value)
    {
        _isResponseToInput = value;
    }

    public float GetVelocityMagnitude()
    {
        return _ch.velocity.magnitude;
    }

    public Vector3 GetVelocity()
    {
        return _velocity;
    }

    public Vector2 GetHorizontalInput()
    {
        return _horizontalInput;
    }

    public float GetHorizontalSpeed()
    {
        return new Vector3(_velocity.x, 0, _velocity.z).magnitude;
    }

    public bool IsGrounded()
    {
        return _grounded;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
