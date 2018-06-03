using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        RaycastHit2D hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.right.normalized * controller.enemyStats.attackRange, Color.red);

        hit = Physics2D.CircleCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.right, controller.enemyStats.attackRange, controller.layer);


        if (hit.collider != null)
        {
            if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate)){
                controller.launcher.Fire(controller.enemyStats.attackRate);
            }
        }
 
    }
}