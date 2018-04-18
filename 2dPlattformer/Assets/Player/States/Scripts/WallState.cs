using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Wall")]
public class WallState : State
{
    private PlayerController _controller;
    public float GlideSpeed;
    public float WallCheckDistance = 0.15f;
    private Vector2 _wallNormal;
    private Vector2 _inputDirection;
    private Vector2 _wallHitPoint;
    private Transform _transform { get { return _controller.transform; } }

    [Header("Jumping")]
    public Vector2 WallJumpSpeed;
    public float FallOffSpeed;
    public float InitialJumpDistance;


    private Vector2 Velocity
    {
        get { return _controller.Velocity; }
        set { _controller.Velocity = value; }
    }

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }


    public override void Update()
    {
        UpdateInput();
        UpdateCollision();
        UpdateVelocity();
        _transform.position += (Vector3)Velocity * Time.deltaTime;
        WallJump();
    }

    private void UpdateInput()
    {
        float input = Input.GetAxisRaw("Horizontal");
        _inputDirection = Mathf.Abs(input) < _controller.InputMagnitudeToMove ? Vector2.zero :
       input > 0.0f ? Vector2.right : -Vector2.right;
    }

    private void UpdateCollision()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(_transform.position + (Vector3)
       _controller.Collider.offset, _controller.Collider.size, 0.0f, _inputDirection,
       WallCheckDistance, _controller.CollisionLayers);

        if (hits.Length == 0) {
            _controller.GetState<AirState>().CanCancelJump = true;
            _controller.TransitionTo<AirState>();
            return;
        }

        _controller.SnapToHit(hits[0]);
        _wallNormal = Vector2.zero;
        _wallHitPoint = Vector2.zero;

        foreach (RaycastHit2D hit in hits)
        {
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
            {
                _controller.TransitionTo<GroundState>();
            }

            if (MathHelper.GetWallAngleDelta(hit.normal) >
                _controller.MaxWallAngleDelta)
            {
                _controller.GetState<AirState>().CanCancelJump = true;
                _controller.TransitionTo<AirState>();
            }

            _wallNormal += hit.normal;
            _wallHitPoint += hit.point;
        }
        _wallNormal /= hits.Length;
        _wallHitPoint /= hits.Length;
    }

    private void UpdateVelocity()
    {
        float rotation = Vector2.Dot(_wallNormal, Vector2.right) < 0.0 ? 90 : -90;
        Vector2 direction = MathHelper.RotateVector(_wallNormal, rotation);
        Velocity = direction * GlideSpeed;
    }

    private void WallJump()
    {
        Vector2 directionToWall = (_wallHitPoint - (Vector2)_transform.position).normalized;
        if (Vector2.Dot(directionToWall, _inputDirection) < 0.0f)
            Jump(new Vector2(FallOffSpeed, Velocity.y));
        else if (Input.GetButtonDown("Jump"))
            Jump(WallJumpSpeed);
    }
    private void Jump(Vector2 speed)
    {
        Velocity = new Vector2(MathHelper.Sign(_wallNormal.x) * speed.x, speed.y);
        _transform.position += (Vector3)_wallNormal * InitialJumpDistance;
        _controller.GetState<AirState>().CanCancelJump = true;
        _controller.TransitionTo<AirState>();
    }


}

