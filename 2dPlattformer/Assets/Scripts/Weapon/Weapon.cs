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

    public GameObject armed;

    public AudioClip[] machineGun, gunShot;
    public AudioSource source;

    public bool isShotgun = false;

    
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint? WHAT?!?");
        }
    }

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

        if (ammo == 0 && (armed.GetComponentInParent<Armed>().selectedWeapon == 1 || armed.GetComponentInParent<Armed>().selectedWeapon == 2))
        {
            ammo = 999999999;
            armed.GetComponentInParent<Armed>().selectedWeapon = 0;

        }
    }

    void ProjectileShoot()
    {
        if (isShotgun)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject bullet = ObjectPooler.sharedInstance.GetPooledObject("PlayerBullet");
                if (bullet != null)
                {
                    bullet.transform.position = firePoint.transform.position + new Vector3(0, i);
                    bullet.transform.rotation = firePoint.transform.rotation;
                    bullet.SetActive(true);
                }
            }
        }

        if(!isShotgun)
        {
            GameObject bullet = ObjectPooler.sharedInstance.GetPooledObject("PlayerBullet");
            if (bullet != null)
            {
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.rotation = firePoint.transform.rotation;
                bullet.SetActive(true);
            }
        }
        

        if (armed.GetComponentInParent<Armed>().selectedWeapon == 1)
        {

            RandomSound(machineGun);
            ammo--;
        }

        if (armed.GetComponentInParent<Armed>().selectedWeapon == 2)
        {

            //RandomSound(machineGun); Lägga in ljud för shotgun
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