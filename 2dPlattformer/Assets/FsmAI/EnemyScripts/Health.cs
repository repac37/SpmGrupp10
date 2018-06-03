using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public EnemyStats enemyStats;
    private float _currentHealth;

    void Start () {
        _currentHealth = enemyStats.startingHealth;
    }

    public void HitDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 1)
        {
            Destroy(gameObject);
        }
    }


}
