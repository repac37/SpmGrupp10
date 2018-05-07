using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/DropAttack")]
public class DropAttackStat : State {


    private MiniBossController _controller;
    private float EndState;
    private float sinceLastAction;

    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
        _controller.Gravity = -30;
    }

    public override void Enter()
    {
        EndState = 5;
        sinceLastAction = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        sinceLastAction += Time.deltaTime;
        //Debug.Log(sinceLastAction + ":" + EndState);
        if(sinceLastAction > EndState)
        {
            _controller.TransitionTo<BetweenIdle01To02>();
        }


        if (_controller.collisions.above || _controller.collisions.below)
        {
            _controller.Velocity.y = 0;
        }
        _controller.Velocity.y += _controller.Gravity * Time.deltaTime;
        _controller.Move(_controller.Velocity * Time.deltaTime);
    }

  
}
