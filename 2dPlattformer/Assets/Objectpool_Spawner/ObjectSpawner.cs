using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    private Transform spawnPoint;
    private GameObject enemy;

    public string typeOfEnemyToSpawn; //Viktigt att fienderna finns i objectpoolen annars hittas dom ej.

    public int numberOfEnemysToSpawn;

    public List<GameObject> spawnedEnemies;


    void Start()
    {
        spawnPoint = transform.Find("SpawnPoint");
        spawnedEnemies = new List<GameObject>();
    }

    void Update()
    {
        RemoveDeadEnemies();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < numberOfEnemysToSpawn; i++)
            {
                enemy = ObjectPooler.sharedInstance.GetPooledObject(typeOfEnemyToSpawn);
                if (enemy != null)
                {
                    SpawnEnemy(enemy);
                    spawnedEnemies.Add(enemy);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DespawnEnemy(enemy);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = spawnPoint.transform.position;
        enemy.transform.rotation = spawnPoint.transform.rotation;
        enemy.SetActive(true);
    }

    private void DespawnEnemy(GameObject enemy)
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (enemy.activeSelf)
            {
                spawnedEnemies[i].SetActive(false);
            }
        }
        EmptyList();
    }

    private void EmptyList()
    {
        spawnedEnemies.Clear();
    }

    private void RemoveDeadEnemies()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (!spawnedEnemies[i].activeSelf)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
    }

}
