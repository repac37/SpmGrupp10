using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShoot : MonoBehaviour {

    public float Damage = 10;
    public LayerMask whatToHit;

    public Transform BulletTrailPrefab;
    public float timer;
    public float startTimer;

    Transform firePoint;
    GameObject objectToHit;

    public DronePatrolState parent;

    // Use this for initialization
    void Awake () {
        timer = startTimer;
        firePoint = transform.Find("FirePoint");
        objectToHit = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0&&parent.detected)
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        Vector2 playerPosition = new Vector2(objectToHit.transform.position.x, objectToHit.transform.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, playerPosition - firePointPosition, 100, whatToHit);

        Effect();

        timer = startTimer;
    }

    void Effect()
    {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}
