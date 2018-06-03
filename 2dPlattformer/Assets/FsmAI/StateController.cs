using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public FSMState currentState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public FSMState remainInState;
    public LayerMask layer;
    public ProjectileLauncher launcher;
    public Movement movement;

    public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;


    // Update is called once per frame
    void Update () {
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
       if(currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        } 
    }

    public void TransitionToState(FSMState nextState)
    {
       
        if (nextState != remainInState)
        {
            Debug.Log("TransitionState: " + nextState.name);
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        Debug.Log("delta "+Time.deltaTime);
        Debug.Log(stateTimeElapsed);
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        Debug.Log("ExitState: " + currentState.name);
        stateTimeElapsed = 0;
    }
}
