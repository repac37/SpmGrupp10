using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    //private PlayerVariables player;


    public Slider fuelSlider;
    public Text ammoText;
    public Sprite[] healthSprites;

    public Image healthUI;
    public int hp;
    public float ammo;
    public float jetfuel = 0;
    public PlayerManager playerManager;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        hp = PlayerManager.currentHealth;



        ammo = Weapon.ammo;
        jetfuel = playerManager.currentFuel;


        fuelSlider.maxValue = playerManager.playerVar.maxFuel;
        fuelSlider.minValue = 0.0f;
        fuelSlider.value = jetfuel;


        if (hp >= 0)
        {
            healthUI.sprite = healthSprites[hp];
        }

        if (ammo > 100)
        {
            ammoText.text = "Ammo: INF";
        }
        else
        {
            ammoText.text = "Ammo: " + ammo.ToString();
        }

    }
}
