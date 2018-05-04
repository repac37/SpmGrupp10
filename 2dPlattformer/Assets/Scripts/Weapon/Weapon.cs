using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public float damage = 10;
    public static float ammo = 9999999999999;



    private float timeToFire = 0;
    private Transform firePoint;

    public Transform bulletTrailPrefab;

    public GameObject armed;

    public AudioClip[] machineGun, gunShot;
    public AudioSource source;

    //public LayerMask whatToHit;

    //public float effectSpawnRate = 10;

    // private float timeToSpawnEffect = 0;


    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint? WHAT?!?");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0 && ammo > 0)
        {
            if (Input.GetAxis("RightTrigger") != 0)
            {
                ProjectileShoot();
            }
        }
        else
        {
            if (Input.GetAxis("RightTrigger") != 0 && Time.time > timeToFire && ammo > 0)
            {
                timeToFire = Time.time + 1 / fireRate;
                ProjectileShoot();
            }
        }

        if (ammo == 0 && armed.GetComponentInParent<Armed>().selectedWeapon == 1)
        {
            ammo = 999999999;
            armed.GetComponentInParent<Armed>().selectedWeapon = 0;

        }
    }

    //void Shoot()
    //{
    //    float inputX = Input.GetAxis("HorizontalRightStick");
    //    float inputY = Input.GetAxis("VerticalRightStick");

    //    Vector2 direction = new Vector2(inputX, inputY);
    //    direction.Normalize();


    //    Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
    //    RaycastHit2D hit = Physics2D.Raycast(firePointPosition, direction, 100, whatToHit);
    //    if (Time.time >= timeToSpawnEffect)
    //    {
    //        Effect();
    //        timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
    //    }
    //    Debug.DrawLine(firePointPosition, direction * 100, Color.cyan);
    //    if (hit.collider != null)
    //    {
    //        Debug.DrawLine(firePointPosition, hit.point, Color.red);
    //        Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
    //        Destroy(hit.collider.gameObject);
    //    }
    //}

    void ProjectileShoot()
    {

        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);

        if (armed.GetComponentInParent<Armed>().selectedWeapon == 1)
        {

            RandomSound(machineGun);
            ammo--;
        }

        if (armed.GetComponentInParent<Armed>().selectedWeapon == 0)
        {

            RandomSound(gunShot);
        }

    }

    void RandomSound(AudioClip[] sounds)
    {
        int coll = Random.Range(1, sounds.Length);
        AudioClip clip = sounds[coll];
        //  krocka.pitch = pitchSpeed * 0.5f;
        source.PlayOneShot(sounds[coll]);
        sounds[coll] = sounds[0];
        sounds[0] = clip;
        //   kwater.volume = 5.2f;
    }
}