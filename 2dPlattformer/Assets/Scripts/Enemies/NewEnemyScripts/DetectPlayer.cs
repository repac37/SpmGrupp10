using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    public EnemyShoot enemyShoot;


    private void Start()
    {
        enemyShoot = GetComponentInParent<EnemyShoot>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyShoot.isPatroling = false;
            enemyShoot.canShoot = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyShoot.canShoot = false;
        }
    }
}
