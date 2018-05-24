using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weekPoint : MonoBehaviour {

    public GameObject parentEnemy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")&& other.gameObject.GetComponent<BulletScript>().playerBullet)
        {
            parentEnemy.GetComponent<ShieldManager>().curentShieldHealth += -1;
        }
    }
}
