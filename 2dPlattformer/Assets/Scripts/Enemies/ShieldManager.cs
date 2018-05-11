using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour {

	public GameObject enemyHealth;
    public GameObject shield;
    public int aktivateShield;
    public GameObject weekPoint;
	public GameObject weekPoint2;
	public GameObject weekPoint3;
    public int curentShieldHealth;
    public int shieldHealth;

	public int state;

    public float timer;
    //public float weekpointTimer;
    public float startTimer;

	public int health;

    // Use this for initialization
    void Start () {
		state = 0;
        timer = 0;
        //weekpointTimer = timer;
		health = enemyHealth.GetComponent<EnemyManager> ().currentHealth;
    }
	
	// Update is called once per frame
	void Update () {
		health = enemyHealth.GetComponent<EnemyManager> ().currentHealth;
		if (health == 0)
        {
            Destroy(gameObject);
        }
        if (health<=aktivateShield&&shield!=null)
        {
            //Debug.Log(curentShieldHealth+" "+timer);
            if (timer <= 0)
            {
				
                shield.SetActive(true);
				if(state==0)
					weekPoint.SetActive(true);
				if(state != 0){
					if (weekPoint2 != null) {
                        //if(weekpointTimer<=0)
						    weekPoint2.SetActive (true);
					}
					if (weekPoint3 != null) {
                        //if (weekpointTimer <= 0)
                            weekPoint3.SetActive (true);
					}
				}
                timer = startTimer;
                //weekpointTimer = timer;                
                curentShieldHealth = shieldHealth ;
            }

            if (curentShieldHealth==0)
            {
                
                shield.SetActive(false);
				if(state==0)
                	weekPoint.SetActive(false);
				if(state != 0){
					if (weekPoint2 != null) {
                        
						    weekPoint2.SetActive (false);
					}
					if (weekPoint3 != null) {
                        
                            weekPoint3.SetActive (false);
					}
				}
                timer -= Time.deltaTime;

            }
        }
	}
}
