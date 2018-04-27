using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{



    public bool horizontal = true;
    public float speed = 1f;
    public float sinOffset = 3f;
    public GameObject playerContanier = null;

    public Vector2 velocity;
    private Vector3 _lastPosition;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (horizontal)
            transform.position += transform.right * Mathf.Sin(Time.time * sinOffset) * speed;
        else
            transform.position += transform.up * Mathf.Sin(Time.time * sinOffset) * speed;

        Velocity();

    }

    private void Velocity()
    {
        velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("enter");
            // child the player to the trigger parent:
            collision.transform.parent = this.transform;
         }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("exit");
            // unchild the player from the platform
            collision.transform.parent = playerContanier.transform;
        }
    }

}
