using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public Transform crosshair;



    void Start()
    {
        Transform clone = Instantiate(crosshair, new Vector2(transform.position.x + 5, transform.position.y), Quaternion.identity);
        crosshair = clone;
        crosshair.transform.parent = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("HorizontalRightStick");
        float inputY = Input.GetAxis("VerticalRightStick");
        if (Mathf.Abs(inputX) > 0.2f || Mathf.Abs(inputY) > 0.2f)
        {
            crosshair.gameObject.SetActive(true);
            Vector2 difference = new Vector2(inputX, inputY);
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            


        }
        else
        {
            crosshair.gameObject.SetActive(false);
       
        }
    }
}
