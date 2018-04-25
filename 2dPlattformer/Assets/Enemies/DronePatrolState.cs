using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrolState : MonoBehaviour
{
    public float movementSpeed;
    public float startWaitTime;
    private float waitTime;

    public Transform[] patrolPoints;
    private int randomSpot;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, patrolPoints.Length);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[randomSpot].position, movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[randomSpot].position) < 0.5f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, patrolPoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

}
