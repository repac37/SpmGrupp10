using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    public PlayerVariables playerVar;
    public float currentFuel;
    public bool isRefuel = false;
    public static int currentHealth;
    public GameObject player;



    public LevelManager _levelManager;

    void Start()
    {
        currentHealth = playerVar.health;
        currentFuel = playerVar.maxFuel;

        _levelManager = FindObjectOfType<LevelManager>(); 
    }

    public void Update()
    {
        Damage();
        Refuel(isRefuel);
    }

    public void Refuel(bool isRefuel)
    {
        if (!isRefuel) return;
        if (currentFuel >= playerVar.maxFuel) return;
        currentFuel += playerVar.RefuelRate;
    }

    public void Fuel(float cost)
    {
        if (currentFuel >= 0)
        {
            currentFuel -= cost;
        }

    }


    public void Damage()
    {
        if (currentHealth < 1)
        {
            //StartCoroutine(Die());
            Respawn();
        }

    }

    IEnumerator Die()
    {
        //player.SetActive(false);
        yield return new WaitForSeconds(2f);
        Respawn();

        SceneManager.LoadScene(0);   
    }

    public void Respawn()
    {

        //SceneManager.LoadScene("TestLevel01");
        
        _levelManager.RespawnPlayer();
    }

    public void ResetPlayer()
    {
        currentHealth = playerVar.health;
    }
    

}
