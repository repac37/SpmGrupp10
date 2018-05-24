using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAim : MonoBehaviour {

    public GameObject objectToHit;
    public int rotationOffset = 0;
    EnemyShoot enemyShoot;



    // Use this for initialization
    void Start () {
        enemyShoot = GetComponent<EnemyShoot>();
        objectToHit = enemyShoot.objectToHit;

    }

    private void Update()
    {

        if (!enemyShoot.isPatroling)
        {
            objectToHit = enemyShoot.objectToHit;
            if (objectToHit != null) { 
            Vector3 playerPos = new Vector3(objectToHit.transform.position.x, objectToHit.transform.position.y);
            Vector3 difference = playerPos - transform.position;
            difference.Normalize();
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }
        }
     
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    
        if (other.gameObject.CompareTag("Player"))
        {
            enemyShoot.isPatroling = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            enemyShoot.isPatroling = true;
        }
    }
}
