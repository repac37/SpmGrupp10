using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingRotation : MonoBehaviour {
    GameObject objectToHit;
    public int rotationOffset = 0;

    public DronePatrolState parent;


    private void Start()
    {
        objectToHit = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.detected)
        {
            objectToHit = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPos = new Vector3(objectToHit.transform.position.x, objectToHit.transform.position.y);
            Vector3 difference = playerPos - transform.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }


       
    }
}
