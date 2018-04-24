using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{

    public int rotationOffset = 0;

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("HorizontalRightStick");
        float inputY = Input.GetAxis("VerticalRightStick");
        if (inputX != 0.0f && inputY != 0.0f)
        {
            Vector2 difference = new Vector2(inputX, inputY);

          
            //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0 + rotationOffset);
        }
    }
}
