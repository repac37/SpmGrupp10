using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartMusic : MonoBehaviour {
    public AudioMixerSnapshot start;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            //running = true;
            start.TransitionTo(4.0f);
            
        }


    }
}
