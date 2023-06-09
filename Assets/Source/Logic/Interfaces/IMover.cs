﻿using System;
using UnityEngine;


public interface IMover
{
    public void PerformMove();
    public void SetHorizontalInput(Vector2 input);
    public void SetVerticalVelocity(float velocity);
    public void SetMaxSpeed(float value);
    public void AddVerticalVelocity(float addedVelocity);
    public void SetVelocity(Vector3 newVelocity);
    public void SetHorizontalVelocity(Vector3 newVelocity);
    public void AddVelocity(Vector3 addedVelocityVector);
    public void SetInputResponse(bool value);
    
    public Vector3 GetVelocity();
    public Vector2 GetHorizontalInput();
    public float GetVelocityMagnitude();
    public float GetHorizontalSpeed();
    public bool IsGrounded();
}
