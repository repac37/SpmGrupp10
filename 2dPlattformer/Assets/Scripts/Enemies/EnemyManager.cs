using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int health;
    public int currentHealth;
    public int shield = 0;
    public bool hasShield = false;
    public bool TakeDamage = true;

    public bool spawnedEnemy = false;

    public WeaponData weapon;

    public Renderer rend;
    public Material mat1, mat2, mat3, mat4;

    public float deathTimer = 0.3f;
    public float damageTimer = 0.1f;


    private void Start()
    {
        rend.material = mat1;
        currentHealth = health;
        if (shield > 0)
        {
            hasShield = true;
        }

 
    }


    public void HitDamage(int damage)
    {
        if (TakeDamage)
        {
            if (hasShield && shield > 0)
            {
                shield -= damage;

                if (shield == 0)
                    hasShield = false;
            }

            if (!hasShield && health > 0)
            {
                currentHealth -= damage;
                if (currentHealth == 0) { 
                //if (spawnedEnemy)
                //    Destroy(this.gameObject);
                //else if (!spawnedEnemy)
                //    gameObject.SetActive(false);
                KillOrHide();
                }
                else if (currentHealth != 0)
                {
                    StartCoroutine(DamageColor());
                }
            }
            
        }
    }

    public void KillOrHide()
    {
 
         StartCoroutine(DeathColor());

        
    }

    IEnumerator DeathColor()
    {
        if (rend != null)
        {
            rend.material = mat2;
        }
        if (gameObject.GetComponent<BoxCollider2D>())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<CircleCollider2D>())
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        yield return new WaitForSeconds(deathTimer);

        if (spawnedEnemy)
        {
            Destroy(gameObject);
        }
        else if (!spawnedEnemy)
        {           
            gameObject.SetActive(false);           

        }

        
        
       
    }
    IEnumerator DamageColor()
    {
        rend.material = mat3;
        yield return new WaitForSeconds(damageTimer);
        rend.material = mat1;

    }

    IEnumerator ColliderColor()
    {
        rend.material = mat4;
        yield return new WaitForSeconds(damageTimer);
        rend.material = mat1;

    }

    public void ResetEnemy()
    {
        gameObject.SetActive(true);
        if (gameObject.GetComponent<BoxCollider2D>())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (gameObject.GetComponent<CircleCollider2D>())
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        rend.material = mat1;
        currentHealth = health;
    }
    public float meleCountdown;
    public float meleCountdownStart=1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material = mat4;
            PlayerManager.currentHealth--;
            //ColliderColor();
            meleCountdown = meleCountdownStart;
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            meleCountdown -= Time.deltaTime;
            if (meleCountdown<=0)
            {
                PlayerManager.currentHealth--;
                //punshSound
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mat4 != null)
        {
            rend.material = mat1;
            //meleCountdown = meleCountdownStart;
        }
    }

}
