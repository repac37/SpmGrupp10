using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
   

    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        Vector3 distance = (controller.chaseTarget.position - controller.transform.position);
        Vector3 direction = distance.normalized;
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        controller.movement.Move(direction.normalized, controller.enemyStats.attackSpeed, zRotation);

    }
}
