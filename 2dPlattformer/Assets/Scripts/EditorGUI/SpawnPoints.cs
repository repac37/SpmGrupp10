using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

    public float explosionRadius = 5.0f;
    public float offsetX = 0;
    public float offsetY = 0;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
       
        Gizmos.DrawSphere(GetSpawPosition(), explosionRadius);
    }

    public Vector3 GetSpawPosition()
    {
        return transform.position + new Vector3(offsetX, offsetY);
    }
}
