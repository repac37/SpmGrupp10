using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int health;

	public bool spawnEnemy = false;

    /*public GameObject shield;
    public int aktivateShield;
    public GameObject weekPoint;
    public int curentShieldHealth;
    public int shieldHealth;
	
    public float timer;
    public float startTimer;
	*/

    // Use this for initialization
    void Start () {
        //timer = 0;
		//plasera fiende i lista om inte en spawnEnemy
        
    }
	
	// Update is called once per frame
	void Update () {

        if (health == 0)
        {
			if (spawnEnemy)
				Destroy (gameObject);
			else if (!spawnEnemy)
				gameObject.SetActive (false);
				
        }
        /*if (health<=aktivateShield&&shield!=null)
        {
            //Debug.Log(curentShieldHealth+" "+timer);
            if (timer <= 0)
            {
                shield.SetActive(true);
                weekPoint.SetActive(true);
                timer = startTimer;
                curentShieldHealth = shieldHealth ;
            }

            if (curentShieldHealth==0)
            {
                
                shield.SetActive(false);
                weekPoint.SetActive(false);
                timer -= Time.deltaTime;

            }
		}*/
	}
}
