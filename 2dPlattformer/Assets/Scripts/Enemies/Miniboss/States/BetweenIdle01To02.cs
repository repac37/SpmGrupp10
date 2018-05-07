using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/Between")]
public class BetweenIdle01To02 : State {

    public bool shield;
    public Color shieldColor = Color.cyan;
    public Transform shieldTrigger;
    public ShieldDownMiniBoss shieldDownMiniBoss;
    public bool shieldCoroutine;
    private Renderer _bodyRender;
    private MiniBossController _controller;

    private Vector2 _moveTo;
    public float moveSpeed = 10f;

    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
        Transform _body = _controller.transform.Find("Body");
        _bodyRender = _body.gameObject.GetComponent<Renderer>();
        shieldTrigger = _controller.transform.Find("SecondeState");
        shieldTrigger = shieldTrigger.transform.Find("ShieldTrigger");
        shieldDownMiniBoss = shieldTrigger.GetComponent<ShieldDownMiniBoss>();
    }

    public override void Enter()
    {
        _bodyRender.material.color = Color.cyan;
        shieldTrigger.gameObject.SetActive(true);
        _controller.manager.TakeDamage = false;
        shieldCoroutine = false;
        _moveTo = _controller.PatrolPoints[1].position;
        
    }

    public override void Update()
    {
        MovetoPosition();
        _controller.Move(_controller.Velocity * Time.deltaTime);
    }

    private void MovetoPosition()
    {
       
        if (Vector2.Distance(_controller.transform.position, _moveTo) > 1f)
        {

            Vector2 dir = (_moveTo - (Vector2)_controller.transform.position).normalized;
            _controller.Velocity = dir * moveSpeed;
        }
        else
        {
            _controller.TransitionTo<MiniBossIdle02>();
        }

    }

    public override void Exit()
    {
        _controller.Velocity = Vector2.zero;
    }
}
