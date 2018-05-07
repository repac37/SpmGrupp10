using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse {
    public float xAxis;
    public float yAxis;

    public Ellipse (Vector2 offsetPosition)
    {
        xAxis = offsetPosition.x;
        yAxis = offsetPosition.y;

    }

    public Vector2 Evaluate(float t)
    {
        float angle = Mathf.Deg2Rad * 360 * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;

        return new Vector2(x, y);

        
    }


}
