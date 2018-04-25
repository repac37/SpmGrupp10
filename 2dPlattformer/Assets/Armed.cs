using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armed : MonoBehaviour {


    public int selectedWeapon = 0;


    // Use this for initialization
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            selectedWeapon = 0;
        }

        Debug.Log("Vapen " + selectedWeapon);

        SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
