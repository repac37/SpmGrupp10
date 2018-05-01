using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFollowState : MonoBehaviour {

    public DronePatrolState parent;

    void Start()
    {
        //dronePatrolState = GameObject.Find("DroneEnemy");
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parent.detected = true;
            parent.canShoot = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parent.canShoot = false;
        }
    }
}
