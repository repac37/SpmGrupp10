using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public GameObject instance;
    public PlayerVariables playerVar;
    public float currentFuel;
    public bool isRefuel = false;
    public static int playerCurrentHealth;
    public GameObject player;
    //public Slider fuelSlider;
    public Image fuelValue;
    public Image JetPackBar;

    public int jetPack;
    public static string jetPackKey = "Jetpack";

    // public Animator anim;


    public GameObject[] aktivationList;
    public GameObject[] deaktivationList;

    public bool regularDeath = true;

    public LevelManager _levelManager;

    public PlayerController controlerToDeactivate;

    public AudioClip shotDeath, electricDeath;
    public AudioSource src;
    public AudioMixerGroup playerDeath;

    void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        playerCurrentHealth = playerVar.health;
        currentFuel = playerVar.maxFuel;
        controlerToDeactivate = FindObjectOfType<PlayerController>();

    }
    private void Start()
    {
        jetPack = GameManager.jetPack;
        if (jetPack == 1)
        {
            //fuelSlider.gameObject.SetActive(true);
            JetPackBar.gameObject.SetActive(true);
        }
        else
        {
            //fuelSlider.gameObject.SetActive(false);
            JetPackBar.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        playerCurrentHealth = playerVar.health;
        ResetSprites();
        jetPack = GameManager.jetPack;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ActivateJetPack();
        }

    }

    public void ActivateJetPack()
    {
        GameManager.jetPack = 1;
        //fuelSlider.gameObject.SetActive(true);
        JetPackBar.gameObject.SetActive(true);
        jetPack = 1;
    }

    public void Refuel()
    {

        if (currentFuel >= playerVar.maxFuel)
        {
            currentFuel = playerVar.maxFuel;
            return;
        }
        currentFuel += playerVar.RefuelRate * Time.deltaTime;

        //fuelSlider.value = currentFuel;
        fuelValue.fillAmount = MathHelper.Scale(0f, playerVar.maxFuel, 0f, 1f, currentFuel);
    }

    public void Fuel()
    {

        if (currentFuel >= 0)
        {
            //Debug.Log("cost: "+cost);
            currentFuel -= playerVar.jetPackFuelCost * Time.deltaTime;
            //fuelSlider.value = currentFuel;
            fuelValue.fillAmount = MathHelper.Scale(0f, playerVar.maxFuel, 0f, 1f, currentFuel);

        }

    }

    public void Damage()
    {
        playerCurrentHealth--;
        if (playerCurrentHealth < 1)
        {
            SetDeathAnimation();
            //Respawn();
        }
    }

    IEnumerator Die()
    {
        //player.SetActive(false);
        controlerToDeactivate.enabled = false;
        if (regularDeath)
            yield return new WaitForSeconds(shotDeath.length);
        else if (!regularDeath)
            yield return new WaitForSeconds(electricDeath.length);
        controlerToDeactivate.enabled = true;
        Respawn();

        //SceneManager.LoadScene(0);   
    }

    public void SetDeathAnimation()
    {
        for (int i = 0; i < aktivationList.Length; i++)
        {
            if (regularDeath)
            {
                aktivationList[0].SetActive(true);
                if (!src.isPlaying)
                {
                    src.PlayOneShot(shotDeath);
                }
            }

            else if (!regularDeath)
            {
                aktivationList[1].SetActive(true);

                if (!src.isPlaying)
                {
                    src.PlayOneShot(electricDeath);
                }

            }
        }
        for (int i = 0; i < deaktivationList.Length; i++)
        {
            deaktivationList[i].SetActive(false);
        }
        //anim.SetInteger("Health", playerCurrentHealth);

        StartCoroutine(Die());
        //Respawn();
    }


    public void Respawn()
    {
        //SceneManager.LoadScene("TestLevel01");
        if (SceneManager.GetActiveScene().name.Equals("Level4"))
            SceneManager.LoadScene("Level4");
        else
        {
            //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("ShotDead"))
            _levelManager.RespawnPlayer();
        }
    }

    public void FullHealth()
    {
        playerCurrentHealth = playerVar.health;
        regularDeath = true;
        //anim.SetInteger("Health", currentHealth);

        for (int i = 0; i < aktivationList.Length; i++)
        {
            aktivationList[i].SetActive(false);
        }
        for (int i = 0; i < deaktivationList.Length; i++)
        {
            deaktivationList[i].SetActive(true);
        }
    }

    public void ResetSprites()
    {

        regularDeath = true;

        for (int i = 0; i < aktivationList.Length; i++)
        {
            aktivationList[i].SetActive(false);
        }
        for (int i = 0; i < deaktivationList.Length; i++)
        {
            deaktivationList[i].SetActive(true);
        }

    }




}
