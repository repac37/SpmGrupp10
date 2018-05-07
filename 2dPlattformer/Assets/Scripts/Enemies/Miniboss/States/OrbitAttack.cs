using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/OrbitAttack")]
public class OrbitAttack : State
{
    //public Transform orbitingObjekt;
    public float xSpread;
    public float ySpread = 10;

    public Transform centerPoint;

    public float rotSpeed;
    public bool rotateClockwise;
    float timer = 0;
    private MiniBossController _controller;
    // Use this for initialization
    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
       
       

    }
    public override void Enter()
    {
        centerPoint = _controller.PatrolPoints[1].transform;
        xSpread = Vector2.Distance(centerPoint.position, _controller.transform.position);
    }
    // Update is called once per frame
    public override void Update()
    {
        timer += Time.deltaTime * rotSpeed;
        Rotate();
    }

    void Rotate()
    {
        if (rotateClockwise)
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float y = Mathf.Sin(timer) * ySpread;
            Vector2 pos = new Vector2(x, y);
            _controller.transform.position = (Vector3)pos + centerPoint.position;
        }
        else
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float y = Mathf.Sin(timer) * ySpread;
             Vector2 pos = new Vector2(x, y);
            _controller.transform.position = (Vector3)pos + centerPoint.position;
        }
    }
}
