using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrolState : MonoBehaviour
{

    private Transform player;
    private float speed;
    public float maxFollowSpeed;
    public float minFollowSpeed;
    public float followDistance;

    public float patrolMovementSpeed;
    public float patrolStartWaitTime;
    private float patrolWaitTime;

    public bool detected=false;

    public Transform[] patrolPoints;
    private int randomSpot;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = Random.Range(minFollowSpeed, maxFollowSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (detected)
        {
            followState();
        }
        if(!detected)
        {
            patrolState();
        }

        
    }

    void followState()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void patrolState()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[randomSpot].position) < 0.5f)
        {
            if (patrolWaitTime <= 0)
            {
                randomSpot = Random.Range(0, patrolPoints.Length);
                patrolWaitTime = patrolStartWaitTime;
            }
            else
            {
                patrolWaitTime -= Time.deltaTime;
            }
        }
    }
}
