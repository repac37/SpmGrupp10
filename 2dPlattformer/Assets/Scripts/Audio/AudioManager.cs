using UnityEngine;
using System.Collections;

using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    public AudioClip[] clips = new AudioClip[4];
    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;
    public float jetFuel;
    public AudioClip jetend;
    public AudioSource jetclips;



    public PlayerManager playerManager;

    public AudioMixerGroup jet;
    private float leftTrigger;


    void Start()
    {



        int i = 0;
        while (i < 2)
        {
            GameObject child = new GameObject("AudioPlayer");




            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
            audioSources[i].outputAudioMixerGroup = jet;



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



            nextEventTime += audioSources[flip].clip.length;


            flip = 1 - flip;
        }



    }

    public static void RandomSound(AudioClip[] sounds, AudioSource src)
    {

        int coll = Random.Range(1, sounds.Length);
        AudioClip clip = sounds[coll];
        src.PlayOneShot(sounds[coll]);
        sounds[coll] = sounds[0];
        sounds[0] = clip;

    }

    public void playEngineSound()
    {
        float power = Input.GetAxisRaw("LeftTrigger");




       

        if (jetFuel < 0 || !(power > 0) || playerManager.jetPack == 0)
        {
            audioSources[0].volume = 0f;
            audioSources[1].volume = 0f;

        }
        else
        {
            audioSources[0].volume = power;
            audioSources[1].volume = power;
        }

        if (power > 0 && !jetclips.isPlaying && jetFuel < 0)
            jetclips.PlayOneShot(jetend);

 



    }




}

