using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour {

   
    public int StartHealth = 20;
    public int currentHealth;
    public int shield = 0;
    public bool TakeDamage = false;
    public Image bossHealthValue;
    public Image bossHealthBar;

    public Color shieldColor = new Color(255, 0, 255);
    public Color bossColor = new Color(255, 0,0);
    public List<Sprite> bossSprites;
    public SpriteRenderer bodyRender;
    public ShieldDownMiniBoss sheild;
 
    public bool bossDead;

    private void Awake()
    {
        
        sheild = GetComponentInChildren<ShieldDownMiniBoss>();
        bodyRender = transform.Find("Body").GetComponent<SpriteRenderer>();
        bossHealthValue.fillAmount = MathHelper.Scale(0f, StartHealth, 0f, 1f, StartHealth);
        bossHealthBar.gameObject.SetActive(false);
    }

    private void Start()
    {
       
        Arena.bossdead = false;
        bossHealthBar.gameObject.SetActive(true);
        currentHealth = StartHealth;
    }

    private void OnEnable()
    {
        Debug.Log("enable manager");
        Arena.bossdead = false;
        bossHealthBar.gameObject.SetActive(true);
        currentHealth = StartHealth;
    }

    private void OnDisable()
    {
        if(bossHealthBar!=null)
            bossHealthBar.gameObject.SetActive(false);
    }

    public void HitDamage(int damage)
    {
      
        if (TakeDamage)
        {
            if (currentHealth > 0)
            {
                currentHealth -= damage;
                StartCoroutine(HitAni());
            }
            else
            {
                
                bossHealthBar.gameObject.SetActive(false);
                Arena.bossdead = true;
                Debug.Log(Arena.bossdead);
                bodyRender.sprite = bossSprites[2];
                StopCoroutine(HitAni());
            }
        }

        bossHealthValue.fillAmount = MathHelper.Scale(0f, StartHealth, 0f, 1f, currentHealth);
    }

    IEnumerator HitAni()
    {
       
        bodyRender.sprite = bossSprites[3];

        yield return new WaitForSeconds(0.3f);

        bodyRender.sprite = bossSprites[0];
    }

    public void setTakeDamage(bool takedamage)
    {
        if (takedamage)
        {
            TakeDamage = true;
            bodyRender.sprite = bossSprites[0];
        }
        else
        {
            TakeDamage = false;
            bodyRender.sprite = bossSprites[1];
        }

    }

}

