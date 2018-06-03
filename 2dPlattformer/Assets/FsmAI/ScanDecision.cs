using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool noEnemyInSight = Scan(controller);
        return noEnemyInSight;
    }

    private bool Scan(StateController controller)
    {
        controller.movement.Move(Vector3.zero, controller.enemyStats.searchingTurnSpeed, Vector3.forward);
        return controller.CheckIfCountDownElapsed(controller.enemyStats.searchDuration);
    }
}
