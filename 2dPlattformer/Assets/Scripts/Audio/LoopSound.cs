using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoopSound : MonoBehaviour
{
    private AudioSource[] audioSources = new AudioSource[2];
    public AudioClip[] clips = new AudioClip[2];
    public AudioSource src1, src2;
    public AudioMixerGroup output;
    private double nextEventTime;
    private int flip = 0;
    private bool running = false;
    // Use this for initialization
    void Start()
    {
        running = true;
        audioSources[0] = src1;
        audioSources[1] = src2;



        nextEventTime = AudioSettings.dspTime;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!running)
            return;

        double time = AudioSettings.dspTime;
        if (time > nextEventTime)
        {
            audioSources[flip].clip = clips[flip];
            audioSources[flip].PlayScheduled(nextEventTime);
            //  Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);


            nextEventTime += audioSources[flip].clip.length;


            flip = 1 - flip;
        }
    }
}