using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/weapon")]
public class WeaponData : ScriptableObject {



    public string hitTarget;
    public Vector2 gravity;
    public Vector2 velocity;
    public int speed;
    public float fireRate;
    public float shootLength;
    public float damage;
    public List<int> ammos;
    public GameObject bullet;
    public float destroyTime;
    public bool playerBullet;
    public bool isInArena;
    public Arena arena;
}
