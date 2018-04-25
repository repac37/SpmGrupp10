using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVariables : MonoBehaviour {

	public float health = 100f;

	[HideInInspector]
	public int coins = 0;
    public int key = 0;
    public int buddy = 0;
	
	private float damageTimer = 0f;

    public int levelNumber;

	private Slider healthSlider;
	private Text coinUI;
	private Vector3 startPos;

    [HideInInspector]
    public bool hasKey = false;

	void Start () {
		//healthSlider = GameObject.Find("HealthSliderUI").GetComponent<Slider>();
		//coinUI = GameObject.Find("CoinTextUI").GetComponent<Text>();
		startPos = transform.position;
	
	}




	void Update () {
		damageTimer += Time.deltaTime;
	
		
		//healthSlider.value = Mathf.Lerp(healthSlider.value, health, Time.deltaTime);
		//coinUI.text = coins+"";
	}

	public void Harm(float dmg){
		if (damageTimer > 1f) {
			health -= dmg;
			damageTimer = 0f;
		}
		// Om damageTimer är större än en sekund bör vi sänka health med damage. Vi bör även sätta damageTimer till 0f för att nollställa timern.KLAR

		if (health < 1f){
			StartCoroutine (Die ());

					
	
		}

			// Om health är mindre än 1f så bör vi starta funktionen Die(). Det kan bara göras med StartCoroutine eftersom Die() är en IEnumerator. YOLO!

	}
	IEnumerator Die() {
		GetComponent<Collider2D>().enabled = false;
		GetComponent<PlayerController>().enabled = false;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,5f),ForceMode2D.Impulse);
		transform.localScale = new Vector3(transform.localScale.x, -1f, 1f);
		
		yield return new WaitForSeconds(2f);
		Respawn ();
		// SceneManager.LoadScene(0); // Load some scene YOLO
		// Eller kalla på Respawn-funktionen vi har gjort? YOLO
	}

	public void Respawn () {

        SceneManager.LoadScene("TestLevel01");

		gameObject.transform.position = startPos;
		// Här nollställer vi ett gäng med variabler för att få spelaren att börja om spelet istället för att helst starta om scenen. YOLO

		// Sätt position, som finns under detta gameObjects transform, till Vector3n startPos. YOLO

		GetComponent<Collider2D>().enabled = true;
		GetComponent<PlayerController>().enabled = true;
		transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);

		health = 100f;
    

		// Sätt tillbaka spelarens hälsa till 100f. YOLO
	}
}
