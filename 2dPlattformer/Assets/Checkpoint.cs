using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;

    public UiManager uiManager;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        uiManager = FindObjectOfType<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            levelManager.currentCheckPoint = gameObject;
            StartCoroutine(WaitForText());
            //Debug.Log("Activated Checkpoint " + transform.position);
        }
    }

    IEnumerator WaitForText()
    {
        uiManager.checkPointText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        uiManager.checkPointText.gameObject.SetActive(false);
    }

}
