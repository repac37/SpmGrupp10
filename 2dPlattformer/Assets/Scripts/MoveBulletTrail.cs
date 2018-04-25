using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletTrail : MonoBehaviour {

    public int moveSpeed = 230;
    public float damage = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D other) //Detta ska ligga på drönares script istället
    {
        //Debug.Log("Player test!");
        if (other.gameObject.tag=="Player")
        {
            //PlayerVariables play = other.gameObject.GetComponent<PlayerVariables>();
            Debug.Log("Player hit!");
            //play.Harm(5f);
                //getComponent<PlayerVariables>().Harm(damage);
            Destroy(gameObject);

            // Kolliderar detta objekt med spelaren bör denna göra via Harm(float) funktionen som finns i spelarens PlayerVariables script.YOLO

        }
    }

   /* private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit!");
            other.GetComponent<PlayerVariables>().Harm(damage);
            Destroy(gameObject);
        }*/
    }


