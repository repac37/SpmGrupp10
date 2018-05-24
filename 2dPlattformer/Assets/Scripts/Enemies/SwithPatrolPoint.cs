using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwithPatrolPoint : MonoBehaviour {

	public GameObject parentEnemy;

	public Transform[] stage1PatrolPoints;
	public Transform[] stage2PatrolPoints;

	public int healthSwitch; 
	public int health;

	public bool aktivatedStage=false;

	//public float timer;
	//public float startTimer;

	// Use this for initialization
	void Start () {
		//timer = startTimer;
		parentEnemy.GetComponent<DronePatrolState> ().patrolPoints = stage1PatrolPoints;
		aktivatedStage=false;
	}
	
	// Update is called once per frame
	void Update () {
		health = parentEnemy.GetComponent<EnemyManager> ().startHealth;
		//Debug.Log(parentEnemy.GetComponent<EnemyManager>().currentHealth+" "+healthSwitch+" "+aktivatedStage);
		//timer -= Time.deltaTime;
		if(parentEnemy.GetComponent<EnemyManager>().currentHealth==healthSwitch&&!aktivatedStage){
		//if(timer<=0){
			//healthSwitch+=10;
			aktivatedStage=true;
		
			if (parentEnemy.GetComponent<DronePatrolState> ().patrolPoints == stage1PatrolPoints) {
				parentEnemy.GetComponent<DronePatrolState> ().patrolPoints = stage2PatrolPoints;
				parentEnemy.GetComponent<ShieldManager> ().state = 1;
			}
			else if(parentEnemy.GetComponent<DronePatrolState> ().patrolPoints == stage2PatrolPoints)
				parentEnemy.GetComponent<DronePatrolState> ().patrolPoints = stage1PatrolPoints;
			//timer = startTimer;
		}
		//}

	}
}
