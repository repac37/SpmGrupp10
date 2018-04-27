using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public GameObject armed;
    public GameObject weapon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            armed.GetComponentInParent<Armed>().selectedWeapon = 1;

            gameObject.SetActive(false);

            Debug.Log("Byta vapen");

            if (armed.GetComponentInParent<Armed>().selectedWeapon == 1)
            {
               // weapon.GetComponentInParent<Weapon>().ammo = 100;
            }

        }
    }
}
