using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    public Transform firePoint;
    public GameObject projecttilePrefab;
    private float nextFireTime;


    public void Fire(float fireRate)
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            var projectileInstance = Instantiate(projecttilePrefab,
            firePoint.position,
            firePoint.rotation);

        }
    }
}
