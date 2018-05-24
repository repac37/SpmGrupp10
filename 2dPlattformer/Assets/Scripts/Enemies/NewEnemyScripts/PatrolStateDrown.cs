using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyShoot))]
public class PatrolStateDrone : MonoBehaviour {

    public EnemyShoot enemyShoot;
    private GameObject player;

    public float maxFollowSpeed = 5;
    public float minFollowSpeed = 3;
    public float followDistance = 8;

    public float patrolMovementSpeed = 3;
    public float patrolStartWaitTime = 0;
    private float patrolWaitTime;

    private float speed;

    public List<Transform> patrolPoints;
    private int randomSpot;
    

    private void Start()
    {
        enemyShoot = GetComponentInParent<EnemyShoot>();
   
        speed = Random.Range(minFollowSpeed, maxFollowSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        if (!enemyShoot.isPatroling)
        {
            FollowState();
        }
        else
        {
            if (patrolPoints.Count != 0)
                patrolState();

        }

    }

    void FollowState()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemyShoot.objectToHit.transform.position, speed * Time.deltaTime);
    }

    void patrolState()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[randomSpot].position) < 0.5f)
        {
            if (patrolWaitTime <= 0)
            {
                randomSpot = Random.Range(0, patrolPoints.Count);
                patrolWaitTime = patrolStartWaitTime;
            }
            else
            {
                patrolWaitTime -= Time.deltaTime;
            }
        }
    }
}
