using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{

    public Transform crosshair;
    public float rotationSpeed = 10;
    public float aimOffset = 5;
    public SpriteRenderer armRender;
    public SpriteRenderer legRender;
    public SpriteRenderer torsoRender;
    public SpriteRenderer jetRender;
    public GameObject jetFire;

    void Awake()
    {

        crosshair.position = crosshair.position + new Vector3(5f, 0f, 0f);
        crosshair.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        AimRotation();

    }

    private void AimRotation()
    {

        float inputX = Input.GetAxis("HorizontalRightStick");
        float inputY = Input.GetAxis("VerticalRightStick");

        if (Mathf.Abs(inputX) > 0.4f || Mathf.Abs(inputY) > 0.4f)
        {
            crosshair.gameObject.SetActive(true);
            Vector2 difference = new Vector2(inputX, inputY);
            difference.Normalize();
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rotZ), Time.deltaTime * rotationSpeed);

            if ((transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 280))
            {
                if (armRender.flipY == false)
                {
                    armRender.flipY = true;
                    legRender.flipX = true;
                    torsoRender.flipX = true;
                    jetRender.flipX = true;
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x * -1f, gameObject.transform.localPosition.y, 0);
                    jetFire.transform.localPosition = new Vector3(jetFire.transform.localPosition.x + 1.2f, jetFire.transform.localPosition.y, 0);
                    jetRender.transform.localPosition = new Vector3(0.35f, jetRender.transform.localPosition.y, 0);
                }
            }
            else
            {
                if (armRender.flipY == true)
                {
                    armRender.flipY = false;
                    legRender.flipX = false;
                    torsoRender.flipX = false;
                    jetRender.flipX = false;
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x * -1f, gameObject.transform.localPosition.y, 0);
                    jetFire.transform.localPosition = new Vector3(jetFire.transform.localPosition.x - 1.2f, jetFire.transform.localPosition.y, 0);
                    jetRender.transform.localPosition = new Vector3(-0.35f, jetRender.transform.localPosition.y, 0);
                }
            }
        }
        else
        {
            crosshair.gameObject.SetActive(false);

        }
    }
}
