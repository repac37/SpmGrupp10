using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Arena : MonoBehaviour
{
    [Header("Arena")]
    public CameraFollow cam;
    //private CameraFollow originalCamera;
    public GameObject cameraPosition;
    //private Vector3 Position;
    public GameObject lazer;
    public GameObject lazer2;
    public GameObject exitLazer;
    public GameObject exitLazer2;
    public int killcount;
    public int killcount2;
    public int savedKillcount;
    public int savedKillcount2;
    public int arenaCameraSize = 10;
    public float sizeSpeed = 0.1f;

  
    [Header("Boss")]
    public bool bossArena = false;
    public GameObject boss;
    public GameObject bossTmp;
    public Transform Spawnpoint;
    public static bool bossdead;

    public AudioSource open_close;
    public AudioClip open_closeSound;
    public AudioMixerGroup output;

    public ObjectPooler pool;

    public LevelManager levelManager;
    private IEnumerator coroutine;

    private void Start()
    {
        open_close.outputAudioMixerGroup = output;
        levelManager = FindObjectOfType<LevelManager>();
        cam = FindObjectOfType<CameraFollow>();

        savedKillcount = killcount;
        savedKillcount2 = killcount2;
        cam.arenaCameraSize = arenaCameraSize;
        cam.sizeSpeed = sizeSpeed;

        if (lazer != null)
        {
            lazer.SetActive(false);

        }
        if (lazer2 != null)
        {
            lazer2.SetActive(false);

        }
    }

    private void PlaySound()
    {
        if (!open_close.isPlaying)
            open_close.PlayOneShot(open_closeSound);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coroutine = ActivateArean();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator ActivateArean()
    {
        yield return new WaitForSeconds(0.5f);
        levelManager.ResetEnemies();

        foreach (GameObject p in pool.pooledObjects)
        {
            p.SetActive(false);
        }

        cam.enterdArena = true;
        cam.arenaCameraSize = arenaCameraSize;
        cam.sizeSpeed = sizeSpeed;

        if (lazer != null)
        {
            lazer.SetActive(true);
            PlaySound();
        }
        if (lazer2 != null)
        {
            lazer2.SetActive(true);
            PlaySound();
        }

        if (bossArena)
        {
            bossTmp = Instantiate(boss, Spawnpoint);
            bossTmp.SetActive(true);
        }
    }

    public IEnumerator ResetArena()
    {
        yield return new WaitForSeconds(0.5f);
        if (exitLazer != null)
        {
            exitLazer.SetActive(true);
            PlaySound();
        }
        if (exitLazer2 != null)
        {
            exitLazer2.SetActive(true);
            PlaySound();
        }
        if (lazer != null)
        {
            lazer.SetActive(false);

        }
        if (lazer2 != null)
        {
            lazer2.SetActive(false);

        }
        killcount = savedKillcount;
        killcount2 = savedKillcount2;
        cam.enterdArena = false;
       // Debug.Log(cam.enterdArena);

        if (bossArena)
        {
            Destroy(bossTmp);
            Arena.bossdead = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam._targetPosition = cameraPosition.transform.position;
        }

        if (killcount == 0 && exitLazer != null && !bossArena)
        {
            if (!open_close.isPlaying)
                open_close.PlayOneShot(open_closeSound);
            exitLazer.SetActive(false);
        }

        if (killcount == killcount2 && exitLazer2 != null && !bossArena)
        {
            exitLazer2.SetActive(false);
            if (!open_close.isPlaying)
                open_close.PlayOneShot(open_closeSound);
        }
  
        if (bossdead)
        {
            if (!open_close.isPlaying)
                open_close.PlayOneShot(open_closeSound);
            exitLazer.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            StopCoroutine(coroutine);
            // Spelaren har triggat cameratrigger
            cam.enterdArena = false;

            coroutine = ResetArena();
            StartCoroutine(coroutine);

        }
    }
    public void ResetArenaCoroutine()
    {
        StartCoroutine(ResetArena());
    }
}