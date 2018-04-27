using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GroundState")]
public class GroundState : State {

    private PlayerController _controller;

    [Header("Movement")]
    public float Acceleration;
    public float Deceleration;
    public float TurnModifier;

    //private bool onMovingPlateForm;
    //private Vector2 hitVelocity;

    [Header("Jumping")]
    public int MaxJumps = 2;
    public float InitialJumpDistance;
    public MinMaxFloat JumpHeight;
    public float TimeToJumpApex;
    [HideInInspector]
    public MinMaxFloat JumpVelocity; // bestäms av gravitation och TimeToJumpApex

    [Header("Jetpack")]
    public float reloadJetpack = 1f;
    private float _JetpackCountDownTimer = 0;

    private int _jumps;
    private Vector2 _groundNormal;
    private Vector2 _vectorAlongGround
    {
        get
        {
            return MathHelper.RotateVector(_groundNormal, -90f);
        }
    }
    private Transform _transform { get { return _controller.transform; } }
    private Vector2 _velocity
    {
        get { return _controller.Velocity; }
        set { _controller.Velocity = value; }
    }

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
        //bestämmer gravitation efter hopp höjden och hopptiden 
        _controller.Gravity = (2 * JumpHeight.Max) / (TimeToJumpApex* TimeToJumpApex);
        //bestämmer hoppet max hastighet
        JumpVelocity.Max = _controller.Gravity * TimeToJumpApex;
        JumpVelocity.Min = Mathf.Sqrt(2 * _controller.Gravity * JumpHeight.Min);
    }

    public override void Enter()
    {
        _jumps = MaxJumps;
        //PlayerController.fuel = _controller.GetState<JetpackState>().MaxJetPackFuel;


    }

    public override void Update()
    {
       
        UpdateGravity();
        RaycastHit2D[] hits = _controller.DetectHits(true);
        if (hits.Length == 0)
        {
            TransitionToAir();
            return;
        }
        //ytnormal
        UpdateGroundNormal(hits);
        //rörelse normalen
        UpdateMovement();
        UpdateNormalForce(hits);
        _transform.Translate(_velocity * Time.deltaTime);
      
        
        UpdateJump();
        UpdateJetpack();

    }

    private void UpdateGravity()
    {
        _velocity += Vector2.down * _controller.Gravity * Time.deltaTime;
    }

    private void UpdateGroundNormal(RaycastHit2D[] hits)
    {
        int numberOfGroundHits = 0;
        _groundNormal = Vector2.zero;
        foreach (RaycastHit2D hit in hits)
        {
            //if (hit.collider.CompareTag("MovingPlatform"))
            //{
            //    onMovingPlateForm = true;
            //    hitVelocity = hit.collider.GetComponent<MovablePlatform>().velocity;
            //    float dirX = Math.Sign(hitVelocity.x);
            //    float dirY = Math.Sign(hitVelocity.y);
            //    float pushX = (dirY == 1) ? hitVelocity.x : 0;
            //    float pushY = hitVelocity.y - hit.distance * dirY;
            //    _velocity = new Vector2(pushX, pushY);
            //}
            //Stämmer av lutningen om den är inom vår ram fortsätter.
            if (!MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                continue;

            _groundNormal += hit.normal;
            numberOfGroundHits++;
        }
        if (numberOfGroundHits == 0)
        {
            TransitionToAir();
            return;
        }

        _groundNormal /= numberOfGroundHits;
        _groundNormal.Normalize();
    }

    private void UpdateNormalForce(RaycastHit2D[] hits)
    {
        if (hits.Length == 0) return;

        _controller.SnapToHit(hits[0]);


        foreach (RaycastHit2D hit in hits)
        {
            if (Mathf.Approximately(_velocity.magnitude, 0.0f)) continue;

      
                _velocity += MathHelper.GetNormalForce(_velocity, hit.normal);
            
        }
    }

    private void UpdateMovement()
    {
        if (_groundNormal.magnitude < MathHelper.FloatEpsilon) return;

        float input = Input.GetAxisRaw("Horizontal");
        //Ökar eller sänker hastigheten
        if (Mathf.Abs(input) > _controller.InputMagnitudeToMove)
        {
            Accelerate(input);
        }
        else
        {
            Decelerate();
        }
    }

    private void Accelerate(float input)
    {
        //kollar rikting
        int direction = MathHelper.Sign(_velocity.x);
        // till för att öka acceleration vi vändning
        float turnModifier = MathHelper.Sign(input) != direction && direction != 0 ? TurnModifier : 1f;

        Vector2 deltaVelocity = _vectorAlongGround * input * Acceleration * turnModifier * Time.deltaTime;

        Vector2 newVelocity = _velocity + deltaVelocity;
        // kollar om den ny acceleration är större än max hastighet
        if(newVelocity.magnitude > _controller.MaxSpeed)
        {
            _velocity = _vectorAlongGround * MathHelper.Sign(_velocity.x) * _controller.MaxSpeed;
        }
        else
        {
            _velocity = newVelocity; 
        }
        
    }

    private void Decelerate()
    {
        Vector2 deltaVelocity = MathHelper.Sign(_velocity.x) * _vectorAlongGround * Deceleration * Time.deltaTime;
        Vector2 newVelocity = _velocity - deltaVelocity;

        if (_velocity.magnitude < MathHelper.FloatEpsilon ||
            MathHelper.Sign(newVelocity.x) != MathHelper.Sign(_velocity.x))
        {

            _velocity = Vector2.zero;
        }
        else
        {
            _velocity = newVelocity;
        }
    }

    public void UpdateJump()
    {
        if (!Input.GetButtonDown("LeftBumper") || _jumps <= 0) return;
       
            _transform.position += Vector3.up * InitialJumpDistance;
            _controller.Velocity.y = JumpVelocity.Max;
            _controller.GetState<AirState>().CanCancelJump = true;
            TransitionToAir();
   
    }

    private void TransitionToAir()
    {
        _jumps--;
        _controller.TransitionTo<AirState>();
    }

    public void UpdateJetpack()
    {
        UpdateFuel();
        float VerticalInput = Input.GetAxis("Vertical");
     
        if (Input.GetAxis("LeftTrigger") == 0 || VerticalInput < 0 ) return;
        TransitionToJetPack();
    }

    private void TransitionToJetPack()
    {
        //_controller.TransitionTo<JetpackState>();
        if (_JetpackCountDownTimer <= 0)
        {
            _JetpackCountDownTimer = reloadJetpack;
            _controller.GetState<JetpackState>().currentFuel = _controller.GetState<JetpackState>().MaxJetPackFuel;
            PlayerController.fuel = _controller.GetState<JetpackState>().currentFuel;
            _controller.TransitionTo<JetpackState>();
        }
    }

    private void UpdateFuel()
    {
        
        if (_JetpackCountDownTimer >= 0)
        {
            _JetpackCountDownTimer -= 1f * Time.deltaTime;
            
        }
    }

    public override void Exit()
    {
        //onMovingPlateForm = false;
    }

}
