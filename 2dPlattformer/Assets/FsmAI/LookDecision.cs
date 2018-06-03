using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit2D hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.right.normalized * controller.enemyStats.lookRange,Color.green);

        hit = Physics2D.CircleCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.right, controller.enemyStats.lookRange, controller.layer);
      

        if (hit.collider != null)
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
