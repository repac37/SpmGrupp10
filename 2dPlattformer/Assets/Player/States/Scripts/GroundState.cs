using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundState : State
{
    public int MaxJumps = 2;

    private int _jumps;
    private PlayerController _controller;
    

    [Header("Movement")]
    public float Acceleration;
    public float Deceleration;
    public float TurnModifier;

    [Header("Jumping")]
    public MinMaxFloat JumpVelocity;
    public float InitialJumpDistance;
    public MinMaxFloat JumpHeight;
    public float TimeToApexJump;



    private Vector2 _groundNormal;
    private Vector2 VectorAlongGround { get { return MathHelper.RotateVector(_groundNormal, -90f); } }
    private Transform _transform { get { return _controller.transform; } }
    private Vector2 _velocity
    {
        get { return _controller.Velocity; }
        set { _controller.Velocity = value; }
    }

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
        _controller.Gravity = (2 * JumpHeight.Max) / Mathf.Pow(TimeToApexJump, 2);
        JumpVelocity.Max = _controller.Gravity * TimeToApexJump;
        JumpVelocity.Min = Mathf.Sqrt(2 * _controller.Gravity * JumpHeight.Min);
    }

    public override void Enter()
    {
        _jumps = MaxJumps;
    }

    public override void Update()
    {
        UpdateGravity();
        RaycastHit2D[] hits = _controller.DetectHits(true);
        if (hits.Length == 0)
        {
            _jumps--;

            _controller.TransitionTo<AirState>();
            return;
        }
        UpdateGroundNormal(hits);
        UpdateMovement();
        UpdateNormalForce(hits);
        _transform.Translate(_velocity * Time.deltaTime);

        //RunAnimation
        
        UpdateJump();
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
            if (!MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                continue;
            _groundNormal += hit.normal;
            numberOfGroundHits++;
        }

        if (numberOfGroundHits == 0)
        {
            _jumps--;
            _controller.TransitionTo<AirState>();
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
            if (Mathf.Approximately(_velocity.magnitude, 0.0f))
                continue;

            _velocity += MathHelper.GetNormalForce(_velocity, hit.normal);
            
        }
    }

    private void UpdateMovement()
    {
        if (_groundNormal.magnitude < MathHelper.FloatEpsilon) return;
        float input = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(input) > _controller.InputMagnitudeToMove)
            Accelerate(input);
        else
            Decelerate();
    }

    private void Accelerate(float input)
    {
        int direction = MathHelper.Sign(_velocity.x);
        float turnModifier = MathHelper.Sign(input) != direction && direction != 0 ? TurnModifier : 1f;

        Vector2 deltaVelocity = VectorAlongGround * input * Acceleration * turnModifier * Time.deltaTime;
        Vector2 newVelocity = _velocity + deltaVelocity;
        _velocity = newVelocity.magnitude > _controller.MaxSpeed ? VectorAlongGround * MathHelper.Sign(_velocity.x) * _controller.MaxSpeed : newVelocity;
    }

    private void Decelerate()
    {
        Vector2 deltaVelocity = MathHelper.Sign(_velocity.x) * VectorAlongGround * Deceleration * Time.deltaTime;
        Vector2 newVelocity = deltaVelocity - deltaVelocity;
        _velocity = _velocity.magnitude < MathHelper.FloatEpsilon || MathHelper.Sign(newVelocity.x) != MathHelper.Sign(_velocity.x) ? Vector2.zero : newVelocity;
    }

    public void UpdateJump()
    {
        if (!Input.GetButtonDown("Jump") || _jumps <= 0) return;
        _transform.position += Vector3.up * InitialJumpDistance;
        _controller.Velocity.y = JumpVelocity.Max;
        _jumps--;
        _controller.GetState<AirState>().CanCancelJump = true;

        if (!(_controller.CurrentState is AirState))
            _controller.TransitionTo<AirState>();
    }

}
