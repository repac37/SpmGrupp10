using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager: MonoBehaviour
{
    //private PlayerVariables player;


    //private PlayerVariables player;


    //public Slider fuelSlider;

    public Text ammoText;
    public Sprite[] healthSprites;

    public Text checkPointText;

    public Image healthUI;
    public int hp;
    public float ammo;
    //public float jetfuel = 0;
    public PlayerManager playerManager;


    // Use this for initialization
    void Start()
    {
        playerManager = (PlayerManager)FindObjectOfType(typeof(PlayerManager));
        if(checkPointText.gameObject!=null)
            checkPointText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        hp = PlayerManager.playerCurrentHealth;

        ammo = Gun.ammo;



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
