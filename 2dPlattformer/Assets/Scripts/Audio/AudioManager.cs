﻿using UnityEngine;
using System.Collections;


public class AudioManager : MonoBehaviour
{

    public AudioClip[] clips = new AudioClip[4];
    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;
    public float minDelay, maxDelay;
    private float jetFuel;
    public AudioClip jetMain;
    public AudioSource jet1, jet2;

    public PlayerManager playerManager;







    void Start()
    {

        //audioSources[0].clip = jetMain;
        //audioSources[1].clip = jetMain;

        int i = 0;
        while (i < 2)
        {
            GameObject child = new GameObject("AudioPlayer");




            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();



            audioSources[i].playOnAwake = false;
            
            i++;
        }

        nextEventTime = AudioSettings.dspTime;
        running = true;


    }
    void Update()
    {
        playEngineSound();
        jetFuel = playerManager.currentFuel;


        if (!running)
            return;

        double time = AudioSettings.dspTime;
        if (time > nextEventTime)
        {
            audioSources[flip].clip = clips[flip];
            audioSources[flip].PlayScheduled(nextEventTime);
            Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);


            nextEventTime += audioSources[flip].clip.length;


            flip = 1 - flip;
        }

        //if (!audioSources[2].isPlaying)
        //{
        //    float d = Random.Range(minDelay, maxDelay);
        //    audioSources[2].PlayDelayed(d);

        //    Debug.Log("sourse[0] d = " + d);
        //}
        //if (!audioSources[3].isPlaying)
        //{
        //    float d = Random.Range(minDelay, maxDelay);
        //    audioSources[3].PlayDelayed(d);
        //    Debug.Log("sourse[1] d = " + d);
        //}

    }

    public static void RandomSound(AudioClip[] sounds, AudioSource src)
    {
 
        int coll = Random.Range(1, sounds.Length);
        AudioClip clip = sounds[coll];
        //  krocka.pitch = pitchSpeed * 0.5f;
        src.PlayOneShot(sounds[coll]);
        sounds[coll] = sounds[0];
        sounds[0] = clip;
        //   kwater.volume = 5.2f;
    }

    public void playEngineSound()
    {
        float power = Input.GetAxisRaw("LeftTrigger");


        //main.pitch = power * 0.8f;


        //if (!jetPackSrc.isPlaying && jetFuel > 0f)
        //{

        //    jetPackSrc.PlayOneShot(jetMain);

        //}

        if (jetFuel < 0 || !(power > 0))
        {
            audioSources[0].volume = 0f;
            audioSources[1].volume = 0f;

        }
        else
        {
            audioSources[0].volume = power/8;
            audioSources[1].volume = power/8;
        }

        //    if (power < 0 && !end.isPlaying)
        //        end.PlayOneShot(endSound);
        //}


    }

}
