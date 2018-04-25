using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerBullet : MonoBehaviour {

    public int moveSpeed = 230;

    public float destroyTime = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
