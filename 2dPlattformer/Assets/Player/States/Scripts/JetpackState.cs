using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "States/JetpakState")]
public class JetpackState : State
{
    private float _gravityTmp;


    [Header("Movement")]
    public float FastFallingModifier = 2f;
    public float Acceleration;
    public float Friction;
    public MinMaxFloat jetPackAcceleration;
    [Header("Fuel")]
    public float MaxJetPackFuel = 4f;
    public float jetPackFuelCost = 2f;
    [HideInInspector] public float currentFuel = 0;

    private bool _hitGround = false;
    public float waitBeforeTransitionToGround = 1.0f;
    private float tmpWaitBeforeTransitionToGround;

    private List<Collider2D> _ignoredPlatforms = new List<Collider2D>();

    private PlayerController _controller;
    private Transform _transform { get { return _controller.transform; } }
    private Vector2 _velocity
    {
        get { return _controller.Velocity; }
        set { _controller.Velocity = value; }
    }

    private Vector2 _hitVelocity;
     
    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        _transform.Translate(Vector3.up * 0.1f);
        _gravityTmp = _controller.Gravity;
        _controller.Gravity = 0;
        tmpWaitBeforeTransitionToGround = waitBeforeTransitionToGround;
    }


    // Update is called once per frame
    public override void Update()
    {

        if (Input.GetAxisRaw("LeftTrigger") != 0)
        {
            _controller.Velocity = Vector2.zero;


            if (currentFuel <= 0)
            {
                _controller.TransitionTo<AirState>();
            }

            //_controller.GetState<GroundState>().UpdateJetpack();
            currentFuel -= jetPackFuelCost * Time.deltaTime;
            //Debug.Log(_controller.GetState<GroundState>().currentFuel);

            RaycastHit2D[] hits = _controller.DetectHits();

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
        else
        {
            _controller.TransitionTo<AirState>();
        }


    }

    private void UpdateNormalForce(RaycastHit2D[] hits)
    {

        if (hits.Length == 0) return; //Kollar om vi träffar nått

        _controller.SnapToHit(hits[0]);  //kollar om vi ska snappa till marken
        
        if (hits[0].collider.CompareTag("Floor"))
        {
            _hitGround = true;
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

           // Debug.Log("jet: " + _velocity);
            _velocity += MathHelper.GetNormalForce(_velocity, hit.normal);
            
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
            {
                if (tmpWaitBeforeTransitionToGround <= 0.0f)
                {
                    _controller.TransitionTo<GroundState>();
                }
            }
        }
    }

    private void UpdateMovement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

    

        if (Mathf.Abs(verticalInput) > _controller.InputMagnitudeToMove)
        {
            if (!_hitGround || (_hitGround && verticalInput >= 0.1))
            {
                Vector2 delta = Vector2.up * (verticalInput == 0 ? 1 : verticalInput) * jetPackAcceleration.Max;
                _velocity += delta;
                _hitGround = false;
                tmpWaitBeforeTransitionToGround = waitBeforeTransitionToGround;
            }
            else
            {
                tmpWaitBeforeTransitionToGround -= 1f * Time.deltaTime;
                Debug.Log("wait: " + tmpWaitBeforeTransitionToGround);
                _velocity = Vector2.zero;
            }
        }
        else if (Mathf.Abs(verticalInput) < _controller.InputMagnitudeToMove && Mathf.Abs(horizontalInput) > _controller.InputMagnitudeToMove)
        {
            _velocity = Vector2.zero;
            tmpWaitBeforeTransitionToGround = waitBeforeTransitionToGround;
        }
        else
        {
            _velocity += Vector2.up * jetPackAcceleration.Min;
            tmpWaitBeforeTransitionToGround = waitBeforeTransitionToGround;
        }


        if (Mathf.Abs(horizontalInput) > _controller.InputMagnitudeToMove)
        {
            Vector2 delta = Vector2.right * (horizontalInput == 0 ? 1 : horizontalInput) * jetPackAcceleration.Max;
            _velocity += delta;
        }

        

    }

    public override void Exit()
    {
        _hitGround = false;
        _controller.Gravity = _gravityTmp;
    }
}