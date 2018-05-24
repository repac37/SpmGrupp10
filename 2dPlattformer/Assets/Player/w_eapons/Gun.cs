using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    
    public float fireRate = 0;
    //public float reloadTime = 0;

    //public float damage = 0;

    //public int currentAmmo;
    //public int maxAmmo;

    public static float ammo = 9999999999999;

    protected float timeToFire = 0;

    //protected bool isReloading = false;

    protected Transform firePoint;

    public AudioClip[] gunSound;
    public AudioSource source;

    //public Equipped eq;

    protected virtual void Awake()
    {
        firePoint = transform.Find("FirePoint");
        //currentAmmo = maxAmmo;
    }

    protected virtual void Update()
    {
        //if (!isReloading)
        //{
        //    if (fireRate == 0 && currentAmmo > 0)
        //    {
        //        if (Input.GetAxis("RightTrigger") != 0)
        //        {
        //            Fire();
        //        }
        //    }
        //    else
        //    {
        //        if (Input.GetAxis("RightTrigger") != 0 && Time.time > timeToFire && currentAmmo > 0)
        //        {
        //            timeToFire = Time.time + 1 / fireRate;
        //            Fire();
        //        }
        //    }
        //    if (currentAmmo <= 0)
        //    {
        //        isReloading = true;
        //        StartCoroutine("Reload");
        //    }
        //}
        //eq = FindObjectOfType<Equipped>();

        if (fireRate == 0 && ammo > 0)
        {
            if (Input.GetAxis("RightTrigger") != 0)
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetAxis("RightTrigger") != 0 && Time.time > timeToFire && ammo > 0)
            {
                timeToFire = Time.time + 1 / fireRate;
                Fire();
            }
        }
       
    }

    protected virtual void Fire()
    {
        GameObject bullet = ObjectPooler.sharedInstance.GetPooledObject("PlayerBullet");
        if (bullet != null)
        {
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.SetActive(true);
        }
        ammo--;

        RandomSound(gunSound);
    }

    //protected virtual IEnumerator Reload()
    //{
    //    yield return new WaitForSeconds(reloadTime);
    //    Debug.Log("Reloading");
    //    currentAmmo = maxAmmo;
    //    isReloading = false;
    //}

    protected void RandomSound(AudioClip[] sounds)
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
