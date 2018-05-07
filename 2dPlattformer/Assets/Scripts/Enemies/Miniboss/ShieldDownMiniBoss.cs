using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDownMiniBoss : MonoBehaviour {

    public bool takeDamage = false;
    public bool isSheild = true; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if(takeDamage)
                isSheild = false;
        }
    }
}
