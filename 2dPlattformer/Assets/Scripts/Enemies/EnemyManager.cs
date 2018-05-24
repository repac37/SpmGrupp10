using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{

    public int startHealth;
    public int currentHealth;
    public int shield = 0;

    public bool hasShield = false;
    public bool TakeDamage = true;

    public bool spawnedEnemy = false;

    public Renderer rend;
    public SpriteRenderer spriteRend;
    public Material mat1, mat2, mat3, mat4;
    public Sprite healthy, dead, damaged, meleAttack;
    public GameObject deadGun;
    public bool boss;

    public float deathTimer = 0.3f;
    public float damageTimer = 0.1f;

    public string randomItemToDrop;
    private int randomNumber;

    private void Start()
    {
        randomNumber = Random.Range(0, 4);

        if (healthy != null)
        {
            spriteRend.sprite = healthy;
        }
        else
        {
            rend.material = mat1;
        }
        currentHealth = startHealth;
        if (shield > 0)
        {
            hasShield = true;
        }

 
    }

    void RandomItem()
    {
        switch (randomNumber)
        {
            case 0:
                randomItemToDrop = "MachinegunPickup";
                Debug.Log("MACHINEGUN!!!");
                break;
            case 1:
                randomItemToDrop = "HealthPickup";
                Debug.Log("HEALTH!!!");
                break;
            case 2:
                randomItemToDrop = "ShotgunPickup";
                Debug.Log("SHOTGUN!!!");
                break;
            default:
                Debug.Log("NO DROP!!!");
                break;
        }
    }

    void DropItem()
    {
        if (randomNumber == 0 || randomNumber == 1 || randomNumber == 2)
        {
            if (!gameObject.activeSelf)
            {
                GameObject pickup = ObjectPooler.sharedInstance.GetPooledObject(randomItemToDrop);
                if (pickup != null)
                {
                    pickup.transform.position = gameObject.transform.position;
                    pickup.transform.rotation = gameObject.transform.rotation;
                    pickup.SetActive(true);
                }
                Debug.Log("Drop!");
            }
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

            if (!hasShield && currentHealth > 0)
            {
                currentHealth -= damage;
                if (currentHealth == 0) { 
  
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
        if (dead != null)
        {
            spriteRend.sprite = dead;
            if (deadGun != null)
                deadGun.SetActive(false);
        }
        else
        {
            if (rend != null)
            {
                rend.material = mat2;
                if (deadGun != null)
                    deadGun.SetActive(false);
            }
        }

        yield return new WaitForSeconds(deathTimer);

        if (spawnedEnemy)
        {
            Destroy(gameObject); //Kommentera in detta och bort med andra för gamla spawners.
            //currentHealth = startHealth; //Kommentera in detta och bort med andra för nya spawners.
            //gameObject.GetComponent<CircleCollider2D>().enabled = true;
            //gameObject.SetActive(false);
        }
        else if (!spawnedEnemy)
        {
            gameObject.SetActive(false);
        }
        if (boss)
        {
            SceneManager.LoadScene("Start");
        }

        RandomItem();
        DropItem(); //Dessa funkar för tillfället bara på fiender som inte är spawned = true; Antar för att spawnade fiender destroyas.

    }

    IEnumerator DamageColor()
    {
        if (damaged != null)
        {
            spriteRend.sprite = damaged;
        }
        else
        {
            rend.material = mat3;
        }

        yield return new WaitForSeconds(damageTimer);

        if (healthy != null)
        {
            spriteRend.sprite = healthy;
        }
        else
        {
            rend.material = mat1;
        }

    }

    IEnumerator ColliderColor()
    {
        if (meleAttack != null)
        {
            spriteRend.sprite = meleAttack;
        }
        else
        {
            rend.material = mat4;
        }

        yield return new WaitForSeconds(damageTimer);
        if (healthy != null)
        {
            spriteRend.sprite = healthy;
        }
        else
        {
            rend.material = mat1;
        }

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
        //rend.material = mat1;
        if (healthy != null)
        {
            spriteRend.sprite = healthy;
        }
        else
        {
            rend.material = mat1;
        }
        if (deadGun != null)
            deadGun.SetActive(true);
        currentHealth = startHealth;
    }

    public float meleCountdown;

    public float meleCountdownStart=1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (meleAttack != null)
            {
                spriteRend.sprite = meleAttack;
            }
            else
            {
                rend.material = mat4;
            }
            //rend.material = mat4;
            collision.gameObject.GetComponent<PlayerManager>().regularDeath = false;
            collision.gameObject.GetComponent<PlayerManager>().Damage();
            //ColliderColor();
            meleCountdown = meleCountdownStart;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meleCountdown -= Time.deltaTime;
            if (meleCountdown <= 0)
            {
                collision.gameObject.GetComponent<PlayerManager>().Damage();
                //punshSound
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && mat4 != null)
        {
            if (healthy != null)
            {
                spriteRend.sprite = healthy;
            }
            else
            {
                rend.material = mat1;
            }
            collision.gameObject.GetComponent<PlayerManager>().regularDeath = true;
            //meleCountdown = meleCountdownStart;
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        rend.material = mat4;
    //        other.gameObject.GetComponent<PlayerManager>().Damage();
    //        //ColliderColor();
    //        meleCountdown = meleCountdownStart;
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D other)
    //{

    //    if (other.CompareTag("Player"))
    //    {
    //        meleCountdown -= Time.deltaTime;
    //        if (meleCountdown<=0)
    //        {
    //            other.gameObject.GetComponent<PlayerManager>().Damage();
    //            //punshSound
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && mat4 != null)
    //    {
    //        rend.material = mat1;
    //        //meleCountdown = meleCountdownStart;
    //    }
    //}

}
