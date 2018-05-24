using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDownMiniBoss : MonoBehaviour {

    public bool hitShield = false;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("hitShield");
            hitShield = true;
        }
    }
}
