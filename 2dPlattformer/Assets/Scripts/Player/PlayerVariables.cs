using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Stats/PlayerVariable")]
public class PlayerVariables : ScriptableObject {

    [Header("Stats")]
    public int health;

    [Header("JetPack")]
    public float maxFuel;
    public float RefuelRate = 0.01f;
}
