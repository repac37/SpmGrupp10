using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action {

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        Vector3 distance = (controller.wayPointList[controller.nextWayPoint].position - controller.transform.position);
        Vector3 direction = distance.normalized;
        controller.movement.Move(direction.normalized,controller.enemyStats.moveSpeed);
        if (distance.magnitude <= 0.1f)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }


}
