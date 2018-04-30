using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/stats")]
public class PlayerManager : ScriptableObject {

    [Header("Stats")]
    public int health;
    public float maxFuel;

}
