using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private bool moveRight = true;

    public void Move(Vector3 movement, float speed)
    {
        if (movement.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (movement.x < 0)
            transform.rotation = Quaternion.Euler(0, 180f, 0);


        transform.position += movement * Time.deltaTime * speed;
    }

    public void Move(Vector3 movement, float speed, Vector3 rotation)
    {
        transform.eulerAngles += rotation*speed*Time.deltaTime;
        transform.position += movement * Time.deltaTime * speed;
    }

    public void Move(Vector3 movement, float speed, float rotation)
    {
        transform.eulerAngles = new Vector3(0,0,rotation);
        transform.position += movement * Time.deltaTime * speed;
    }

}
