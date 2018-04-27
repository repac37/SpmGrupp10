using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerBullet : MonoBehaviour {
     
    public int moveSpeed = 230;

    public float destroyTime = 0;

    public LayerMask layerMask;
    public GameObject dropItem;

    private bool isInArena = false;

    private Arena arena;

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
            //dropItem.transform.position = transform.position;
            //Instantiate(dropItem);
            if (isInArena)
            {
                arena.killcount += -1;
            }
            

            Destroy(gameObject);
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Player")
        {

            Debug.Log("Player hit!");
            Destroy(gameObject);

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena")) {
            isInArena= true;
            arena = collision.GetComponent<Arena>();
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            isInArena = false;
            
        }
    }

}
