using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerVariables : MonoBehaviour {

    public static int health;
    public PlayerManager player;
    [HideInInspector]
	private static float damageTimer = 0f;
    public int levelNumber;
    private Vector3 startPos;
    [HideInInspector]
    public bool hasKey = false;

	void Start () {
        health = player.health;
    }

    void Update () {

        damageTimer += Time.deltaTime;

        if (health < 1)
        {

            StartCoroutine(Die());
        }
    }

	public void Harm(int dmg){
		if (damageTimer > 1f) {
			health -= dmg;
			damageTimer = 0f;
            
		}
		// Om damageTimer är större än en sekund bör vi sänka health med damage. Vi bör även sätta damageTimer till 0f för att nollställa timern.KLAR



			// Om health är mindre än 1f så bör vi starta funktionen Die(). Det kan bara göras med StartCoroutine eftersom Die() är en IEnumerator. YOLO!

	}
	IEnumerator Die() {
		GetComponent<Collider2D>().enabled = false;
		GetComponent<PlayerController>().enabled = false;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,5f),ForceMode2D.Impulse);
		transform.localScale = new Vector3(transform.localScale.x, -1f, 1f);
		
		yield return new WaitForSeconds(2f);
		Respawn ();
		 SceneManager.LoadScene(0); // Load some scene YOLO
		// Eller kalla på Respawn-funktionen vi har gjort? YOLO
	}

	public void Respawn () {

        SceneManager.LoadScene("TestLevel01");

		//gameObject.transform.position = startPos;
		// Här nollställer vi ett gäng med variabler för att få spelaren att börja om spelet istället för att helst starta om scenen. YOLO

		// Sätt position, som finns under detta gameObjects transform, till Vector3n startPos. YOLO

		//GetComponent<Collider2D>().enabled = true;
		//GetComponent<PlayerController>().enabled = true;
		//transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        
		health = 4;
    

		// Sätt tillbaka spelarens hälsa till 100f. YOLO
	}

}
