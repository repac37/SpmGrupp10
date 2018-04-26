using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {
    public CameraFollow cam;
    private CameraFollow originalCamera;
    //private Vector3 Position;
    public GameObject lazer;
    public GameObject exitLazer;
    public int killcount=1;
    public int arenaCameraSize = 10;
    public float sizeSpeed = 0.1f;


    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {Debug.Log("Enter");

            cam.enterdArena = true;
            cam.arenaCameraSize = arenaCameraSize;
            cam.sizeSpeed = sizeSpeed;
            originalCamera = cam;

            lazer.SetActive(true);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam._targetPosition = transform.Find("CameraPosition").position;

            //cam.orthographicSize = cameraSize;

            // Spelaren har triggat cameratrigger
            //cam._targetPosition = new Vector3(cam._targetPosition.x, cam._targetPosition.y, camDepth);
        }
        if(killcount==0){
           exitLazer.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        { Debug.Log("exit");
            // Spelaren har triggat cameratrigger
            cam.enterdArena = false;
            //cam._targetPosition = new Vector3(cam._targetPosition.x, cam._targetPosition.y, cam._targetPosition.z);
        }
    }
}
