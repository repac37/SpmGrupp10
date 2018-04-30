using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public class PlayerManager : MonoBehaviour
{
    public PlayerVariables playerVar;
    public float currentFuel;
    public static int currentHealth;
  

    void Start()
    {
        currentHealth = playerVar.health;
        currentFuel = playerVar.maxFuel;
    }

    public void Update()
    {
        Damage();
    }

    public void Refuel()
    {
        currentFuel = playerVar.maxFuel;
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
            StartCoroutine(Die());
        }

    }

    IEnumerator Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 5f), ForceMode2D.Impulse);
        transform.localScale = new Vector3(transform.localScale.x, -1f, 1f);
        yield return new WaitForSeconds(2f);
        Respawn();
        SceneManager.LoadScene(0); // Load some scene YOLO
                                   // Eller kalla på Respawn-funktionen vi har gjort? YOLO
    }

    public void Respawn()
    {

        SceneManager.LoadScene("TestLevel01");
        currentHealth = playerVar.health;

        //gameObject.transform.position = startPos;
        // Här nollställer vi ett gäng med variabler för att få spelaren att börja om spelet istället för att helst starta om scenen. YOLO

        // Sätt position, som finns under detta gameObjects transform, till Vector3n startPos. YOLO

        //GetComponent<Collider2D>().enabled = true;
        //GetComponent<PlayerController>().enabled = true;
        //transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);




        // Sätt tillbaka spelarens hälsa till 100f. YOLO
    }

}
