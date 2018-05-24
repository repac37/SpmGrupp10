using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{

    public LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerVariables>().Respawn();
            // Spelaren har en funktion som heter Respawn i scriptet PlayerController. Anropa denna funktion för att få spelaren att börja om från sin startposition. YOLO

            if (other.name == "Player")
            {
                other.GetComponent<PlayerManager>().regularDeath = false;
                other.GetComponent<PlayerManager>().SetDeathAnimation();
                //levelManager.RespawnPlayer();
            }

        }
       /* else
        {
            Destroy(other.gameObject);
            // Om det inte är en spelare som kolliderar med denna trigger bör vi ta bort det objektet. YOLO

        }*/
    }
}
