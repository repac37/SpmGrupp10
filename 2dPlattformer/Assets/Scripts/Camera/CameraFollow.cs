using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public PlayerController Player;
    public Vector3 Offset;
    public Vector3 _targetPosition;
    public Camera _orthographicCamera;

    [Header("Follow")]
    public float SmoothingTime;
    private Vector3 _currentVelocity;

    [Header("Look ahead")]
    public float MaxLookAhead;
    public float LookAheadAccelerationTime;
    private float _lookAheadSpeed;
    private float _lookAhead;

    [Header("Look around")]
    public float MaxLookAroundAmount;
    public float TimeBeforeLookAround;
    public float PlayerMaximumSpeedForLookAround;
    private float _playerStillTime;
    private float _lookAroundAmount;

    [Header("BattelArena")]
    public bool enterdArena = false;
    public int arenaCameraSize = 10;
    public int regularCameraSize = 5;
    public float sizeSpeed = 0.1f;
    private float size;

    public void Start()
    {
        size = regularCameraSize;
    }

    public void Update()
    {
        if (!enterdArena)
        {
            //Debug.Log("Innan Lerp: " + arenaCameraSize + " ärvärde: " + regularCameraSize);

            //size = Mathf.Lerp(size, regularCameraSize, t);
            //Debug.Log(Mathf.Lerp(size, regularCameraSize, sizeSpeed));

            //cameraSize = regularCameraSize;
            //uppdateSize();
            UpdateTargetPosition();
            if (size > regularCameraSize)
            {
                size = size - sizeSpeed;
            }

        }
        else
        {
            if (size < arenaCameraSize)
            {
                size = size + sizeSpeed;
            }
            //size = Mathf.Lerp(size, arenaCameraSize, t );
            // cameraSize = arenaCameraSize;
            // uppdateSize();
        }
        //Debug.Log(size);
        _orthographicCamera.orthographicSize = size;



    }

    private void LateUpdate()
    {
        UpdateMovement();
        if (!enterdArena)
        {

            UpdateLookAhead();
            UpdateLookAround();
        }


    }

    private void UpdateTargetPosition()
    {
        _targetPosition = Player.transform.position;
        _targetPosition += Offset;
        _targetPosition += Vector3.right * _lookAhead;
        _targetPosition += Vector3.up * _lookAroundAmount;

    }


    private void UpdateMovement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, SmoothingTime);
    }

    private void UpdateLookAhead()
    {
        float targetLookAhead = MathHelper.Sign(Player.Velocity.x) * MaxLookAhead;
        _lookAhead = Mathf.SmoothDamp(_lookAhead, targetLookAhead,
        ref _lookAheadSpeed, LookAheadAccelerationTime);
    }

    private void UpdateLookAround()
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
