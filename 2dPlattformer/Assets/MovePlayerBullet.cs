using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerBullet : MonoBehaviour {
     
    public int moveSpeed = 230;

    public float destroyTime = 0;

    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            PlayerVariables.health--;
            Destroy(gameObject);
        }

    }
   
}
