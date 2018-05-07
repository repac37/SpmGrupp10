using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    public float damage;
    public float ammo;
    public int moveSpeed;
    public WeaponData guns;
    public GameObject bulletPrefab;
    public Vector2 velocity;
    public List<int> bullets;
    public float shootLength;
    private Transform firePoint;
    private string hitTarget;
    public float destroyTime = 0;

    public bool playerBullet = false;
    private bool isInArena = false;
    private Arena arena;

    // Use this for initialization
    void Awake()
    {

        damage = guns.damage;
        ammo = guns.ammos.Count == 0 ? int.MaxValue : guns.ammos.Count;
        bulletPrefab = guns.bullet;
        firePoint = guns.bullet.transform;
        velocity = guns.velocity;
        bullets = guns.ammos;
        shootLength = guns.shootLength;
        moveSpeed = guns.speed;
        hitTarget = guns.hitTarget;
        destroyTime = guns.destroyTime;
        playerBullet = guns.playerBullet;
        isInArena = guns.isInArena;
        arena = guns.arena;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, destroyTime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hit = other.gameObject;

        if (hit.layer == 8)
        {
            Destroy(gameObject);

        }

        if ((hit.CompareTag("Enemy") || hit.CompareTag("EnemyMove")) && playerBullet)
        {
            EnemyManager manager = hit.GetComponent<EnemyManager>();
            BossManager bossmanager = hit.GetComponent<BossManager>();

            //dropItem.transform.position = transform.position;
            //Instantiate(dropItem);
            if (isInArena)
            {
                arena.killcount += -1;
                //Debug.Log("Arena kill");
            }

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

            Destroy(gameObject);
            //other.GetComponent<EnemyManager>().health--;
            //Destroy(other.gameObject);

        }
        if (hit.gameObject.tag == "Player" && !playerBullet)
        {
            PlayerManager.currentHealth--;
            //Debug.Log("Player hit!");
            Destroy(gameObject);

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
