using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int startingHealth;

    public float moveSpeed;
    public float attackSpeed;
    public float searchingTurnSpeed;
    public float searchDuration;

    public float lookRange;
    public float lookSphereCastRadius;

    public float attackRange;
    public float attackRate;
}
