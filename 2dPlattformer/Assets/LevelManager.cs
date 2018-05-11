using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject startCheckpoint;
    public GameObject currentCheckPoint;

    private PlayerController player;
    private PlayerManager playerMan;
    private Spawner _spawner;

    public List<GameObject> _enemies;
    public List<GameObject> _staticEnemies;
    public List<GameObject> _arenas;
    public List<GameObject> _pickups;

    private EnemyManager enemyManager;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();

        _staticEnemies = new List<GameObject>();

        _arenas = new List<GameObject>();

        player = FindObjectOfType<PlayerController>();

        playerMan = FindObjectOfType<PlayerManager>();

        _spawner = FindObjectOfType<Spawner>();

        enemyManager = FindObjectOfType<EnemyManager>();

        player.transform.position = startCheckpoint.transform.position;

        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Enemy"))
        {

            _staticEnemies.Add(b);

            Debug.Log("Lista: " + _staticEnemies);
        }

        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Arena"))
        {
            _arenas.Add(b);
        }
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Pickup"))
        {
            _pickups.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Reset " + _enemies.Count);
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn");

        player.transform.position = currentCheckPoint.transform.position;

        ResetScene();

    }
    public void ResetScene()
    {
        playerMan.ResetPlayer();
        ResetEnemies();
        ResetArenas();
        for (int i = 0; i < _pickups.Count; i++)
        {
            if (!_pickups[i].activeSelf)
            {
                _pickups[i].SetActive(true);

            }
        }
    }

    public void ResetArenas()
    {
        for (int i = 0; i < _arenas.Count; i++)
        {
            if (_arenas[i].GetComponent<Arena>())
            {
                Debug.Log("ResetArena");
                _arenas[i].GetComponent<Arena>().ResetArena();
            }
            //_arenas[i].SetActive(true);
            if (_arenas[i].GetComponent<ElevatorCamera>())
            {
                Debug.Log("ResetElevator");
                _arenas[i].GetComponent<ElevatorCamera>().ResetArena();
            }

        }
    }
    
    public void ResetEnemies()
    {

        for (int i = 0; i < _enemies.Count; i++)
        {
            //Debug.Log("Reset2");
            _spawner.DestroySpawnedEnemies(_enemies[i]);

        }
        _enemies.Clear();

        for (int i = 0; i < _staticEnemies.Count; i++)
        {
            if (!_staticEnemies[i].activeSelf)
            {
                _staticEnemies[i].GetComponent<EnemyManager>().ResetEnemy();
            }
        }
    }
}
