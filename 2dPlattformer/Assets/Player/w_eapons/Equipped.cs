using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipped : MonoBehaviour {

    public int selectedWeapon = 0;

    private PlayerManager player;
    private Gun weapon;
    public Sprite[] guns;
    public SpriteRenderer spriteRenderer;
    public Transform gunHolder;


    void Awake()
    {
    }

    void Start()
    {
        SelectWeapon();
        player = FindObjectOfType<PlayerManager>();
        spriteRenderer.sprite = guns[0];


    }

    void Update()
    {
        weapon = GetComponentInChildren<Gun>();

        if (Input.GetKeyDown(KeyCode.O))
        {
            spriteRenderer.sprite = guns[0];
            selectedWeapon = 0;
            Gun.ammo = 9999999999999;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            spriteRenderer.sprite = guns[1];
            selectedWeapon = 1;
            Gun.ammo = 100;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            spriteRenderer.sprite = guns[2];
            selectedWeapon = 2;
            Gun.ammo = 60;
        }

        if (Gun.ammo == 0 && (selectedWeapon == 1 || selectedWeapon == 2))
        {
            //isReloading = true;
            //StartCoroutine("Reload");

            Gun.ammo = 999999999;
            selectedWeapon = 0;
            spriteRenderer.sprite = guns[0];
        }

        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in gunHolder.transform)
        {
            if (weapon.GetComponent<Gun>() != null)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MachinegunPickup")
        {
            spriteRenderer.sprite = guns[1];
            selectedWeapon = 1;
            Gun.ammo = 100;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "ShotgunPickup")
        {
            spriteRenderer.sprite = guns[2];
            selectedWeapon = 2;
            Gun.ammo = 60;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "HealthPickup")
        {
            player.FullHealth();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "JetpackPickup")
        {
            player.ActivateJetPack();
            collision.gameObject.SetActive(false);
        }
    }

}
