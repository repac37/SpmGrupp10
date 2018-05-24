using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armed : MonoBehaviour {


    public int selectedWeapon = 0;


    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            selectedWeapon = 1;
            Weapon.ammo = 100;
            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            selectedWeapon = 2;
            Weapon.ammo = 100;

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            selectedWeapon = 0;
            Weapon.ammo = 9999999999999;
        }

        SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if( weapon.GetComponent<Weapon>() != null)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
           
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "WeaponPickup")
        {
            if (selectedWeapon == 1 || selectedWeapon == 2)
            {
                Weapon.ammo = 100;
            }
            other.gameObject.SetActive(false);
        }
    }

}
