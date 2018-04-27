using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "States/AirState")]
public class AirState : State    {

    [Header("Movement")]
    public float FastFallingModifier = 2f;
    public float Acceleration;
    public float Friction;

    public bool CanCancelJump;

    private List<Collider2D> _ignoredPlatforms = new List<Collider2D>();

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
        _ignoredPlatforms.Clear();
    }

    public override void Update()
    {
        if (Input.GetAxis("LeftTrigger") != 0 && _controller.GetState<JetpackState>().currentFuel >= 0)
        {
            _controller.TransitionTo<JetpackState>();
        }
        _controller.GetState<GroundState>().UpdateJump();

        UpdateGravity();
        RaycastHit2D[] hits = _controller.DetectHits();
        CancelJump();
        UpdateMovement();

        Collider2D[] colliders = Physics2D.OverlapBoxAll(_transform.position + (Vector3)_controller.Collider.offset, _controller.Collider.size, 0.0f, _controller.CollisionLayers);
        for (int i = _ignoredPlatforms.Count - 1; i >= 0; i--)
        {
            if (!colliders.Contains(_ignoredPlatforms[i]))
                _ignoredPlatforms.Remove(_ignoredPlatforms[i]);
        }

        UpdateNormalForce(hits);
        _transform.Translate(_velocity * Time.deltaTime);
    }
    private void UpdateGravity()
    {
        float gravityModifier = _velocity.y < 0.0f ? FastFallingModifier : 1f;
        _velocity += Vector2.down * _controller.Gravity * gravityModifier * Time.deltaTime;
    }

    private void UpdateNormalForce(RaycastHit2D[] hits)
    {
        
        if (hits.Length == 0) return; //Kollar om vi träffar nått

       
        RaycastHit2D snapHit = hits.FirstOrDefault(h => !h.collider.CompareTag("OneWay"));
        
        if (snapHit.collider != null)
        {
            _controller.SnapToHit(snapHit);
        }

        //kollar om marken är rätt inom till låten vinkel
        foreach (RaycastHit2D hit in hits)
        {            

            if (hit.collider.CompareTag("OneWay") && _velocity.y > 0.0f && !_ignoredPlatforms.Contains(hit.collider))
            {
                _ignoredPlatforms.Add(hit.collider);
            }

            if (_ignoredPlatforms.Contains(hit.collider))
                continue;

          
           _velocity += MathHelper.GetNormalForce(_velocity, hit.normal);
            
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                _controller.TransitionTo<GroundState>();
        }
    }

    private void UpdateMovement()
    {
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


    private void CancelJump()
    {
        float minJumpVelocity = _controller.GetState<GroundState>().JumpVelocity.Min;
        if (_velocity.y < minJumpVelocity) CanCancelJump = false;
        if (!CanCancelJump || Input.GetButton("LeftBumper")) return;
        CanCancelJump = false;
        _controller.Velocity.y = minJumpVelocity;
    }

}
