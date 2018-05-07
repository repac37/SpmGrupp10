using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossRespawn : MonoBehaviour {

    public LevelManager _levelManager;

	// Use this for initialization
	void Start () {
        _levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        _levelManager.RespawnPlayer();
    }
}
