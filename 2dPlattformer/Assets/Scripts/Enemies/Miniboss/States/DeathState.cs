using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/Death")]
public class DeathState : State {

    private MiniBossController _controller;
    private Renderer _bodyRender;


    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
        _controller.Gravity = -30;
        Transform _body = _controller.transform.Find("Body");
        _bodyRender = _body.gameObject.GetComponent<Renderer>();
    }

    public override void Enter()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {

        if (_controller.collisions.above || _controller.collisions.below)
        {
            _controller.Velocity.y = 0;
            _controller.collisionMask = 0;
            _controller.gameObject.GetComponent<Collider2D>().isTrigger = false;
            _controller.Gravity = 0;
            _bodyRender.material.color = Color.black;
        }
        _controller.Velocity.y += _controller.Gravity * Time.deltaTime;
        _controller.Move(_controller.Velocity * Time.deltaTime);
    }
}
