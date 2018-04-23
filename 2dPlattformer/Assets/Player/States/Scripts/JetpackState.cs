using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/JetpakState")]
public class JetpackState : State
{

    [Header("Movement")]
    public float FastFallingModifier = 2f;
    public float Acceleration;
    public float Friction;
    public float jetPackMaxSpeed = 15.0f;
    public float jetPackAcceleration;
    [Header("Fuel")]
    public float MaxJetPackFuel = 4f;
    public float jetPackFuelCost = 0.25f;
    public float jetPackFuelRegen = 0.15f;

    private PlayerController _controller;
    private Transform _transform { get { return _controller.transform; } }
    private Vector2 _velocity
    {
        get { return _controller.Velocity; }
        set { _controller.Velocity = value; }
    }
    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        UpdateGravity();
        if (Input.GetKey(KeyCode.J))
        {
            if (_controller.GetState<GroundState>().currentFuel <= 0)
            {
                _controller.TransitionTo<AirState>();
            }
            //_controller.GetState<GroundState>().UpdateJetpack();
            _controller.GetState<GroundState>().currentFuel -= jetPackFuelCost * Time.deltaTime;
            Debug.Log(_controller.GetState<GroundState>().currentFuel);
            RaycastHit2D[] hits = _controller.DetectHits();
            //CancelJetpack();
            UpdateNormalForce(hits);
            UpdateMovement();
            _transform.Translate(_velocity * Time.deltaTime);
        }
        else
        {
            _controller.TransitionTo<AirState>();
        }
    }

    private void UpdateGravity()
    {
        float gravityModifier = _velocity.y < 0.0f ? FastFallingModifier : 1f;
        _velocity += Vector2.down * _controller.Gravity * gravityModifier * Time.deltaTime;

    }

    private void UpdateNormalForce(RaycastHit2D[] hits)
    {

        if (hits.Length == 0) return; //Kollar om vi träffar nått

        _controller.SnapToHit(hits[0]);  //kollar om vi ska snappa till marken

        //kollar om marken är rätt inom till låten vinkel
        foreach (RaycastHit2D hit in hits)
        {
            _velocity += MathHelper.GetNormalForce(_velocity, hit.normal);

            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                _controller.TransitionTo<GroundState>();
        }
    }

    private void UpdateMovement()
    {
        if (_controller.Velocity.y <= jetPackMaxSpeed)
        {
            _velocity += Vector2.up*jetPackAcceleration;
        }

        float input = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(input) > _controller.InputMagnitudeToMove)
        {
            Vector2 delta = Vector2.right * input * Acceleration * Time.deltaTime;
            if (Mathf.Abs((_controller.Velocity + delta).x) < _controller.MaxSpeed ||
           Mathf.Abs(_velocity.x) > _controller.MaxSpeed && Vector2.Dot(_velocity.normalized, delta)
           < 0.0f)
                _controller.Velocity += delta;
            else
                _controller.Velocity.x = MathHelper.Sign(input) * _controller.MaxSpeed;
        }
        else
        {
            Vector2 currentDirection = Vector2.right * MathHelper.Sign(_velocity.x);
            float horizontalVelocity = Vector2.Dot(_velocity.normalized, currentDirection) *
           _velocity.magnitude;
            _velocity -= currentDirection * horizontalVelocity * Friction * Time.deltaTime;
        }
    }
}