using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour {


    public BulletStats BulletStats;
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * BulletStats.LaunchForce);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        GameObject hit = other.gameObject;

        if (hit.layer == 8)
        {
            Destroy(gameObject);
            return;
        }
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("newbullet");
            hit.GetComponent<PlayerManager>().Damage();
            gameObject.SetActive(false);
            return;
        }

       
    }
}
