using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour {

    [Header("Jump")]
    public float jumpHeight = 4;
    public float timeToJumpApex = 0.4f;
    public float accelerationTImeAirborne = .2f;

    [Header("Move")]
    public float moveSpeed = 6;
    public float accelerationTimeGround = .1f;


    [Header("Debug")]
    [SerializeField]
    private float jumpVelocity;
    [SerializeField] private float gravity;
    [SerializeField] private Vector2 _velocity;

    private float velocityXSmoothing;
    private MiniBossController _controller;


    private void Start()
    {
        _controller = GetComponent<MiniBossController>();
        gravity = -(jumpHeight * 2) / (timeToJumpApex * timeToJumpApex);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump velocity: " + jumpVelocity);
    }

    private void Update()
    {
        if (_controller.collisions.above || _controller.collisions.below)
        {
            _velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("HorizontalTwo"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && _controller.collisions.below)
        {
            _velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        _velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocityX, ref velocityXSmoothing, (_controller.collisions.below) ? accelerationTimeGround : accelerationTImeAirborne);
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

}
