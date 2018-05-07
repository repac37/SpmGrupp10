using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject startCheckpoint;
    public GameObject currentCheckPoint;

    private PlayerController player;
    private PlayerManager playerMan;
    private Spawner _spawner;

    public List<GameObject> _enemies;

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();

        player = FindObjectOfType<PlayerController>();

        _spawner = FindObjectOfType<Spawner>();

        player.transform.position = startCheckpoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn");
        player.transform.position = currentCheckPoint.transform.position;
        
        for (int i = 0; i < _enemies.Count; i++)
        {
            _spawner.DestroySpawnedEnemies(_enemies[i]);
        }
    }
}
