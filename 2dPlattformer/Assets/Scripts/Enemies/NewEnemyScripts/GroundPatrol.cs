using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPatrol : MonoBehaviour {

    public float speed;
    public float distance;


    public LayerMask hitLayer;
    public Transform groundDetection;
    private Vector2 direction = Vector2.right;

    public EnemyShoot enemyShoot;

    private void Start()
    {
        enemyShoot = transform.Find("Weapon").GetComponent<EnemyShoot>();
    }

    private void Update()
    {
        if (enemyShoot.isPatroling)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, hitLayer);
            RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, direction, distance, hitLayer);

            if (wallInfo.collider == true)
            {
                Flip();
            }

            if (groundInfo == false)
            {
                Flip();
            }
        }
        else
        {
            //FaceingEnemy();
        }

        
    }

    private void FaceingEnemy()
    {
        //throw new NotImplementedException();
    }

    private void Flip()
    {
        speed *= -1;
        direction *= -1;
        groundDetection.localPosition = new Vector2(groundDetection.localPosition.x * -1, groundDetection.localPosition.y);
    }
}
