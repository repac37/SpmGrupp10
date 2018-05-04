using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {
    public CameraFollow cam;
    //private CameraFollow originalCamera;
    public GameObject cameraPosition;
    //private Vector3 Position;
    public GameObject lazer;
    public GameObject lazer2;
    public GameObject exitLazer;
    public GameObject exitLazer2;
    public int killcount=1;
    public int killcount2 = 1;
    public int arenaCameraSize = 10;
    public float sizeSpeed = 0.1f;

    public AudioSource open_close;
    public AudioClip open_closeSound;

    private void Start()
    {
        cam.arenaCameraSize = arenaCameraSize;
        cam.sizeSpeed = sizeSpeed;
    }
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
            // originalCamera = cam;

            if (lazer != null)
            {
                lazer.SetActive(true);
                if (!open_close.isPlaying)
                    open_close.PlayOneShot(open_closeSound);
            }
            if (lazer2!=null)
            {
                lazer2.SetActive(true);
                if (!open_close.isPlaying)
                    open_close.PlayOneShot(open_closeSound);
            }
            
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam._targetPosition = cameraPosition.transform.position;//transform.Find("CameraPosition").position;

            //cam.orthographicSize = cameraSize;

            // Spelaren har triggat cameratrigger
            //cam._targetPosition = new Vector3(cam._targetPosition.x, cam._targetPosition.y, camDepth);
        }
        if(killcount==0 && exitLazer!=null){
           exitLazer.SetActive(false);
            if (!open_close.isPlaying)
                open_close.PlayOneShot(open_closeSound);
        }
        if (killcount == killcount2 && exitLazer2 != null)
        {
            exitLazer2.SetActive(false);
            if(!open_close.isPlaying)
            open_close.PlayOneShot(open_closeSound);
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
