using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public Vector2 elevatorTransform;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(1f);
            other.transform.position = elevatorTransform;

        }
    }
}
