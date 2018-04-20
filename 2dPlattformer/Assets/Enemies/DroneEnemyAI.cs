using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyAI : MonoBehaviour {
    private GameObject player;
    public float ratio = 0.05f;

    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;

    void Start () {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	}
	
	void Update () {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, ratio);
        startPatrolingState();
    }

    public void startPatrolingState()
    {
        //Check if enemy have reached the patrol point
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
        {
            //Check next patrol point and if there are anymore patrol points - if not go back to beginning
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        //Turn to face the current patrol point
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
        transform.Translate(patrolPointDir.normalized * Time.deltaTime * speed);
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;
    }
}
