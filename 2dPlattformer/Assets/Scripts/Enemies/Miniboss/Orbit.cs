using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    
    public Transform center;
    public Ellipse ellipse;
    [Range(0.25f, 0.75f)]
    public float OrbitProgress;
    public float orbitSpeed = 0.01f;
    public bool clockwise;
    void Start () {
        OrbitProgress = 0.25f;
        clockwise = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (clockwise)
        {
            OrbitProgress += Time.deltaTime * orbitSpeed;
            if (OrbitProgress >= 0.75f)
            {
                clockwise = false;
            }
        }
        if (!clockwise)
        {
            OrbitProgress -= Time.deltaTime * orbitSpeed;
            if (OrbitProgress <= 0.25f) 
            {
                clockwise = true;
            }
        }

        //this.gameObject.transform.position = new Vector3(X, Y);
        Vector3 delta = ellipse.Evaluate(OrbitProgress);
        this.gameObject.transform.position = center.position + delta;
    }
}



//public float a, b, x, y, alpha, X, Y;
//public Transform center;
//public float t;
//void Start()
//{

//}

//// Update is called once per frame
//void Update()
//{
//    alpha += 10;
//    X = x + (a * Mathf.Cos(alpha * .005f));
//    Y = y + (b * Mathf.Sin(alpha * .005f));

//    float angle = Mathf.Deg2Rad * 360 * t;
//    float x = Mathf.Sin(angle) * xAxis;
//    float y = Mathf.Cos(angle) * yAxis;
//    //this.gameObject.transform.position = new Vector3(X, Y);
//    Vector2 delta
//        this.gameObject.transform.position = center.position + new Vector3(X, Y);
//}