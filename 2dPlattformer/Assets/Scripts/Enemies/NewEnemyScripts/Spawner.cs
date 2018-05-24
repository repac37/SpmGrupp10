using UnityEngine;
using UnityEngine.Audio;

public class Spawner : MonoBehaviour
{

    public GameObject ReferensEnemy;

    public GameObject spawnArea;


    public Transform[] patrolPoints;

    public bool isInArena;
    public float timer;
    public float startTimer;

    public LevelManager _levelManager;
    public AudioSource src;
    public AudioClip spawn;
    public AudioMixerGroup spawner;

    public Renderer rend;
    public Material mat1, mat2, mat3;
    public SpriteRenderer spriteRend;
    public Sprite idle, aktive, spawning;

    // Use this for initialization
    void Start()
    {
        src.outputAudioMixerGroup = spawner;
        timer = startTimer;

        _levelManager = FindObjectOfType<LevelManager>();

        patrolPoints[0] = transform.Find("PatrolPoint01");
        patrolPoints[1] = transform.Find("PatrolPoint02");

        spriteRend.sprite = idle;
        rend.material = mat1;

    }

    // Update is called once per frame
    void Update()
    {

        if (isInArena)
        {
            timer -= Time.deltaTime;
            rend.material = mat2;
            spriteRend.sprite = aktive;
            Spawn();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            isInArena = true;


        }

    }

    private void Spawn()
    {
        if (timer <= 0.5)
        {
            spriteRend.sprite = spawning;
            rend.material = mat3;
        }
        if (timer <= 0)
        {

            ReferensEnemy.transform.position = spawnArea.transform.localPosition;


            GameObject enemy = Instantiate(ReferensEnemy, transform);
            patrolPoints[1] = transform.Find("PatrolPoint02");
            enemy.GetComponent<PatrolStateDrown>().patrolPoints.Add(patrolPoints[0]);
            enemy.GetComponent<PatrolStateDrown>().patrolPoints.Add(patrolPoints[1]);

            AddToList(enemy);
            timer = startTimer;
            rend.material = mat2;
            spriteRend.sprite = aktive;
            if (!src.isPlaying)
            {
                src.PlayOneShot(spawn);
           
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            if (PlayerManager.playerCurrentHealth <= 0)
            {
                isInArena = false;
                rend.material = mat1;
                spriteRend.sprite = idle;
                timer = startTimer;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInArena = false;
            rend.material = mat1;
            spriteRend.sprite = idle;
            timer = startTimer;
        }
    }
    



    private void AddToList(GameObject referensEnemy)
    {
        _levelManager._enemies.Add(referensEnemy);
    }

    public void DestroySpawnedEnemies(GameObject enemy)
    {
        Destroy(enemy);
    }
}