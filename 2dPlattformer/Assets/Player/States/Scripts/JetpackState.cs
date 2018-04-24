using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/JetpakState")]
public class JetpackState : State
{
    private float _gravityTmp; 
    private float _jetpackTriggerButton;

    [Header("Movement")]
    public float FastFallingModifier = 2f;
    public float Acceleration;
    public float Friction;
    public MinMaxFloat jetPackSpeed;
    public MinMaxFloat jetPackAcceleration;
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
        _transform.position += Vector3.up * _controller.GetState<GroundState>().InitialJumpDistance;
        _controller.Velocity.y = jetPackAcceleration.Max;
        _gravityTmp = _controller.Gravity;
        _controller.Gravity = 0;
    }
 

    // Update is called once per frame
    public override void Update()
    {
    
        _jetpackTriggerButton = Input.GetAxis("LeftTrigger");
       
        if (_jetpackTriggerButton != 0)
        {
            if (_controller.GetState<GroundState>().currentFuel <= 0)
            {
                _controller.TransitionTo<AirState>();
            }

            //_controller.GetState<GroundState>().UpdateJetpack();
            _controller.GetState<GroundState>().currentFuel -= jetPackFuelCost * Time.deltaTime;
            Debug.Log(_controller.GetState<GroundState>().currentFuel);

            RaycastHit2D[] hits = _controller.DetectHits();
       
            UpdateNormalForce(hits);
            UpdateMovement();
            _transform.Translate(_velocity * _jetpackTriggerButton * Time.deltaTime);
        }
        else
        {
            _controller.TransitionTo<AirState>();
        }
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
        //float verticalInput = Input.GetAxisRaw("Vertical");
       
        //if (verticalInput!=0)
        //{
        //    Vector2 delta = Vector2.up * (verticalInput == 0 ? 1 : verticalInput) * jetPackAcceleration.Max * _jetpackTriggerButton;
        //    _velocity = delta;
        //}
        


        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(horizontalInput) > _controller.InputMagnitudeToMove)
        {
            Vector2 delta = Vector2.right * horizontalInput * Acceleration * Time.deltaTime;
            if (Mathf.Abs((_controller.Velocity + delta).x) < _controller.MaxSpeed ||
           Mathf.Abs(_velocity.x) > _controller.MaxSpeed && Vector2.Dot(_velocity.normalized, delta)
           < 0.0f)
                _controller.Velocity += delta;
            else
                _controller.Velocity.x = MathHelper.Sign(horizontalInput) * _controller.MaxSpeed;
        }
        else
        {
            Vector2 currentDirection = Vector2.right * MathHelper.Sign(_velocity.x);
            float horizontalVelocity = Vector2.Dot(_velocity.normalized, currentDirection) *
           _velocity.magnitude;
            _velocity -= currentDirection * horizontalVelocity * Friction * Time.deltaTime;
        }
    }

       public override void Exit()
    {
        _controller.Gravity = _gravityTmp;
    }
}