using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    //public float fallSpeed = 0;

    //private bool isGrounded = false;

    void Start()
    {
    }

    void Update()
    {
        //if (!isGrounded)
        //{
        //    transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.tag == "Floor")
        //{
        //    isGrounded = true;
        //}

        //if (other.gameObject.tag == "PlayerWeapon" || other.gameObject.tag == "Arm" || other.gameObject.tag == "Player")
        //{
        //    isGrounded = false;
        //}
    }

}
