using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/Between")]
public class BetweenIdle01To02 : State {


    public Color shieldColor;

    private MiniBossController _controller;

    private Vector2 _moveTo;
    public float moveSpeed = 10f;

    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
    }

    public override void Enter()
    {
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
        _controller.manager.gameObject.SetActive(true);
        _controller.manager.setTakeDamage(false);
        _moveTo = _controller.PatrolPoints[1].position;
        
    }

    public override void Update()
    {
        MovetoPosition();
        _controller.Move(_controller.Velocity * Time.deltaTime);
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
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
        _controller.manager.setTakeDamage(false);
        _controller.manager.sheild.hitShield = false;
    }
}
