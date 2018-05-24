using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public Transform BulletPrefab;
    public float timer;
    public float startTimer;
    public bool isPatroling = true;
    public Transform firePoint;
    public GameObject objectToHit;
    public bool canShoot = true;
    public LayerMask whatToHit;

    public AudioClip[] shots;
    public AudioSource source;

    void Start()
    {
       
        timer = startTimer;
        firePoint = transform.Find("FirePoint");
        objectToHit = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        
        if (!isPatroling)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
        }
        
        if (timer <= 0)
        {
            if (canShoot)
            {
                if (objectToHit != null) { 
                
                Shoot();
            }
            }

        }
    }

    //private void DebugFunc()
    //{
    //    Vector2 playerPosition = new Vector2(objectToHit.transform.position.x, objectToHit.transform.position.y);
    //    Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
    //    RaycastHit2D hit = Physics2D.Raycast(firePointPosition, playerPosition-firePointPosition, 100, whatToHit);

    //    if (hit.collider.CompareTag("Player"))  
    //        Debug.DrawLine(firePointPosition, playerPosition);
    //}

    public void Shoot()
    {
        
        Vector2 playerPosition = new Vector2(objectToHit.transform.position.x, objectToHit.transform.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, playerPosition - firePointPosition, 100, whatToHit);

        if (hit.collider != null&&hit.collider.CompareTag("Player"))
            CreateBullet();

        timer = startTimer;
    }

    void CreateBullet()
    {
        RandomSound(shots);

        GameObject pickup = ObjectPooler.sharedInstance.GetPooledObject("EnemyBullet");
        if (pickup != null)
        {
            pickup.transform.position = firePoint.transform.position;
            pickup.transform.rotation = firePoint.transform.rotation;
            pickup.SetActive(true);
        }
    }

    void RandomSound(AudioClip[] sounds)
    {
        int coll = Random.Range(1, sounds.Length);
        AudioClip clip = shots[coll];
        //  krocka.pitch = pitchSpeed * 0.5f;
        source.PlayOneShot(sounds[coll]);
        shots[coll] = shots[0];
        shots[0] = clip;
        //   kwater.volume = 5.2f;
    }
}
