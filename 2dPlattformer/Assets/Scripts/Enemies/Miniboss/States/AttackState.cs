using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/AttackOneState")]
public class AttackState : State {

    public Color shieldColor;

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
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
        _startPosition = _controller.transform.position;
        _moveTo = _controller.AttackPoints[0].position;
        _direction = _moveTo - _startPosition;
        _direction.Normalize();
        _isAttacking = true;
        _controller.manager.setTakeDamage(false);
    }

    public override void Update()
    {

 
        if (_controller.collisions.above || _controller.collisions.below)
        {
            _controller.Velocity.y = 0;
        }

        if (_isAttacking)
            UpdateAttackMovement();
        else
            UpdateRedrawMovement();

        _controller.Velocity.y += _controller.Gravity * Time.deltaTime;
        _controller.Move(_controller.Velocity * Time.deltaTime);
        
    }


    public override void Exit()
    {
       
        _controller.Velocity.x = 0;
        _controller.manager.setTakeDamage(true);
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[0];
    }


    private void UpdateAttackMovement()
    {
        
        if (_isAttacking)
        {
            float targetVelocityX = _direction.x * moveSpeed.Max;
            if (MathHelper.GetDistanceInX(_controller.transform.position, _moveTo) < 0.05f)
            {
                _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
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
        _controller.manager.setTakeDamage(true);
        if (!_isAttacking)
        {
            //Debug.Log("isAttacking: " + MathHelper.GetDistanceInX(_controller.transform.position, _startPosition));
            float targetVelocityX = _direction.x * moveSpeed.Min;
            if (MathHelper.GetDistanceInX(_controller.transform.position, _startPosition) > 0.05f)
            {
                _controller.Velocity.x = Mathf.SmoothDamp(_controller.Velocity.x, targetVelocityX, ref _velocityXSmoothing, accelerationTimeGround);
                _controller.GetState<MiniBossIdle01>().ShootRoutine();
            }
            else
            {
                _controller.TransitionTo<MiniBossIdle01>();
            }

        }
    }





}
