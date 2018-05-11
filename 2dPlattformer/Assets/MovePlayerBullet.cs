﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerBullet : MonoBehaviour {
     
    public int moveSpeed = 230;

    //public float destroyTime = 0;

    public LayerMask layerMask;
    public GameObject dropItem;

    public bool playerBullet = false;

    private bool isInArena = false;

    private Arena arena;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        //Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hit = other.gameObject;

        if (hit.layer == 8)
        {
            gameObject.SetActive(false);
        }

        if ((hit.CompareTag("Enemy")|| hit.CompareTag("EnemyMove")) && playerBullet)
        {
            BossManager bossmanager = hit.GetComponent<BossManager>();
            EnemyManager manager = hit.GetComponent<EnemyManager>();
            //dropItem.transform.position = transform.position;
            //Instantiate(dropItem);
            

            try
            {
                manager.HitDamage(1);
                Debug.Log("enemy");
            }
            catch (NullReferenceException e)
            {
                //Debug.Log("Enemy did not have manager.hitDamage()");
            }

            try
            {
                bossmanager.HitDamage(1);
                Debug.Log("boss");
            }
            catch (NullReferenceException e)
            {
                // Debug.Log("Enemy did not have bossmanager.hitDamage()");
            }

            if (isInArena)
            {
                if (other.GetComponent<EnemyManager>().currentHealth==0) {
                    arena.killcount += -1;
                    Debug.Log("Arena kill");

                }
            }


            gameObject.SetActive(false);
            //other.GetComponent<EnemyManager>().health--;
            //Destroy(other.gameObject);

        }
        if (hit.gameObject.tag == "Player"&&!playerBullet)
        {
            PlayerManager.currentHealth--;
            Debug.Log("Player hit!");
            gameObject.SetActive(false);

        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena")) {
            isInArena= true;
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
