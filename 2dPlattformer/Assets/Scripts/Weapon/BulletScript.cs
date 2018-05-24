using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public int moveSpeed = 230;
    public bool playerBullet = false;
    private bool isInArena = false;

    private Arena arena;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        GameObject hit = other.gameObject;

        if (hit.layer == 8)
        {
            gameObject.SetActive(false);
            return;
        }
        if (hit.gameObject.CompareTag("Player") && !playerBullet)
        {
            hit.GetComponent<PlayerManager>().Damage();
            gameObject.SetActive(false);
            return;
        }

        if (playerBullet &&(hit.CompareTag("Enemy") || hit.CompareTag("EnemyMove")))
        {
  
            BossManager bossmanager = hit.GetComponent<BossManager>();
            EnemyManager manager = hit.GetComponent<EnemyManager>();
      
            if (manager != null)
                manager.HitDamage(1);
         
            if(bossmanager!=null)
                bossmanager.HitDamage(1);

            if (manager != null)
            {
                if (isInArena)
                {

                    if (manager.currentHealth == 1  && arena != null)
                    {
                        arena.killcount += -1;
                        Debug.Log("Arena kill");

                    }
                }
            }

            gameObject.SetActive(false);

            return;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            isInArena = true;
            arena = collision.GetComponent<Arena>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            isInArena = false;

        }
    }
}
