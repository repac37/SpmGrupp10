using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/AttackOneState")]
public class AttackState : State {

    [Header("Move")]
    public MinMaxFloat moveSpeed;
    public float accelerationTimeGround = .1f;


    [Header("Distance")]
    public float moveDistance;
    private Vector2 _startPosition;
    private Vector2 _moveTo;
    private Vector2 _direction;
    private bool _isAttacking;

    [Header("Debug")]




    private float _velocityXSmoothing;
    private MiniBossController _controller;


    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
    }

    public override void Enter()
    {
        _startPosition = _controller.transform.position;
        _moveTo = _controller.AttackPoints[0].position;
        _direction = _moveTo - _startPosition;
        _direction.Normalize();
        _isAttacking = true;
        _controller.manager.TakeDamage = false;
    }

    public override void Update()
    {
        

        if (_controller.collisions.above || _controller.collisions.below)
        {
            _controller.Velocity.y = 0;
        }

        if(_isAttacking)
            UpdateAttackMovement();
        else
            UpdateRedrawMovement();

        _controller.Velocity.y += _controller.Gravity * Time.deltaTime;
        _controller.Move(_controller.Velocity * Time.deltaTime);
    }


    public override void Exit()
    {
        _controller.manager.TakeDamage = true;
        _controller.Velocity.x = 0;
    }


    private void UpdateAttackMovement()
    {
        
        if (_isAttacking)
        {
            float targetVelocityX = _direction.x * moveSpeed.Max;
            if (MathHelper.GetDistanceInX(_controller.transform.position, _moveTo) < 0.05f)
            {
                _controller.Velocity.x = Mathf.SmoothDamp(_controller.Velocity.x, targetVelocityX, ref _velocityXSmoothing, accelerationTimeGround);
            }
            else
            {
                _direction =  _startPosition - (Vector2)_controller.transform.position;
                _direction.Normalize();
                _isAttacking = false;
            }
        }
    }

    private void UpdateRedrawMovement()
    {
        if (!_isAttacking)
        {
            //Debug.Log("isAttacking: " + MathHelper.GetDistanceInX(_controller.transform.position, _startPosition));
            float targetVelocityX = _direction.x * moveSpeed.Min;
            if (MathHelper.GetDistanceInX(_controller.transform.position, _startPosition) > 0.05f)
            {
                _controller.Velocity.x = Mathf.SmoothDamp(_controller.Velocity.x, targetVelocityX, ref _velocityXSmoothing, accelerationTimeGround);
            }
            else
            {
                Debug.Log("transition");
                _controller.TransitionTo<MiniBossIdle01>();
            }
        }
    }





}



//[Header("Jump")]
//public float jumpHeight = 4;
//public float timeToJumpApex = 0.4f;
//public float accelerationTimeAirborne = .2f;

//[Header("Move")]
//public float moveSpeed = 6;
//public float accelerationTimeGround = .1f;


//[Header("Debug")]
//[SerializeField]
//private float jumpVelocity;



//private float _velocityXSmoothing;
//private MiniBossController _controller;


//public override void Initialize(Controller owner)
//{
//    _controller = (MiniBossController)owner;
//    _controller.Gravity = -(jumpHeight * 2) / (timeToJumpApex * timeToJumpApex);
//    jumpVelocity = Mathf.Abs(_controller.Gravity) * timeToJumpApex;
//}

//public override void Update()
//{
//    if (_controller.collisions.above || _controller.collisions.below)
//    {
//        _controller.Velocity.y = 0;
//    }

//    Vector2 input = new Vector2(Input.GetAxisRaw("HorizontalTwo"), Input.GetAxisRaw("Vertical"));

//    if (Input.GetKeyDown(KeyCode.Space) && _controller.collisions.below)
//    {
//        _controller.Velocity.y = jumpVelocity;
//    }

//    float targetVelocityX = input.x * moveSpeed;
//    _controller.Velocity.x = Mathf.SmoothDamp(_controller.Velocity.x, targetVelocityX, ref _velocityXSmoothing, (_controller.collisions.below) ? accelerationTimeGround : accelerationTimeAirborne);
//    _controller.Velocity.y += _controller.Gravity * Time.deltaTime;
//    _controller.Move(_controller.Velocity * Time.deltaTime);
//}
