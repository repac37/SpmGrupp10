using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    //public int numberOfPellets = 5;

    public Transform firePoint2;
    public Transform firePoint3;

    public List<Transform> firePoints;

    protected override void Awake()
    {
        firePoint = transform.Find("FirePoint");
        firePoint2 = transform.Find("FirePoint2");
        firePoint3 = transform.Find("FirePoint3");

        //firePoints = new List<Transform>();

        firePoints.Add(firePoint);
        firePoints.Add(firePoint2);
        firePoints.Add(firePoint3);

        //currentAmmo = maxAmmo;

    }

    protected override void Fire()
    {
        for (int i = 0; i < firePoints.Count; i++)
        {
            GameObject bullet = ObjectPooler.sharedInstance.GetPooledObject("PlayerBullet");
            if (bullet != null)
            {
                bullet.transform.position = firePoints[i].transform.position;
                bullet.transform.rotation = firePoints[i].transform.rotation;
                bullet.SetActive(true);
            }
            //currentAmmo--;
            ammo--;
        }
        RandomSound(gunSound);
    }

}
