using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class Ambience : MonoBehaviour
{

    public AudioClip[] clips = new AudioClip[2];
    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[4];
    private bool running = false;
    public float minDelay, maxDelay;
    public AudioClip amb1, amb2;

    public AudioMixerGroup ambLoop, ambRandomSound;










    void Start()
    {



        int i = 0;
        while (i < 4)
        {
            GameObject child = new GameObject("AudioPlayer");




            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();



            audioSources[i].playOnAwake = false;
            i++;
        }

        nextEventTime = AudioSettings.dspTime;
        running = true;
        audioSources[2].clip = amb1;
        audioSources[3].clip = amb2;
        audioSources[0].outputAudioMixerGroup = ambLoop;
        audioSources[1].outputAudioMixerGroup = ambLoop;
        audioSources[2].outputAudioMixerGroup = ambRandomSound;
        audioSources[3].outputAudioMixerGroup = ambRandomSound;


    }
    void Update()
    {
  
        


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

        if (!audioSources[2].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            audioSources[2].PlayDelayed(d);

            Debug.Log("sourse[0] d = " + d);
        }
        if (!audioSources[3].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            audioSources[3].PlayDelayed(d);
            Debug.Log("sourse[1] d = " + d);
        }

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


}

