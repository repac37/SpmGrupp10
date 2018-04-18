using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController Player;
    public Vector3 Offset;
    private Vector3 _targetPosition;    [Header("Follow")]
    public float SmoothingTime;
    private Vector3 _currentVelocity;    [Header("Look ahead")]
    public float MaxLookAhead;
    public float LookAheadAccelerationTime;
    private float _lookAheadSpeed;    private float _lookAhead;    [Header("Look around")]
    public float MaxLookAroundAmount;
    public float TimeBeforeLookAround;
    public float PlayerMaximumSpeedForLookAround;
    private float _playerStillTime;
    private float _lookAroundAmount;    public void Update()
    {
        UpdateTargetPosition();
    }

    private void LateUpdate()
    {
   
        UpdateMovement();
        UpdateLookAhead();
        UpdateLookAround();

    }    private void UpdateTargetPosition()
    {
        _targetPosition = Player.transform.position;
        _targetPosition += Offset;
        _targetPosition += Vector3.right * _lookAhead;
        _targetPosition += Vector3.up * _lookAroundAmount;
    }


    private void UpdateMovement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, SmoothingTime);
    }    private void UpdateLookAhead()
    {
        float targetLookAhead = MathHelper.Sign(Player.Velocity.x) * MaxLookAhead;
        _lookAhead = Mathf.SmoothDamp(_lookAhead, targetLookAhead,
        ref _lookAheadSpeed, LookAheadAccelerationTime);
    }    private void UpdateLookAround()
    {
        if (Player.Velocity.magnitude > PlayerMaximumSpeedForLookAround)
        {
            _lookAroundAmount = 0.0f;
            _playerStillTime = 0.0f;
            return;
        }
        _playerStillTime += Time.deltaTime;
        if (_playerStillTime < TimeBeforeLookAround) return;
        _lookAroundAmount = Input.GetAxisRaw("Vertical") * MaxLookAroundAmount;
    }
}
