using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int health = 1;
    public int shield = 0;
    public bool hasShield = false;
    public bool TakeDamage = true;

    public WeaponData weapon;

    private void Start()
    {
        if (shield > 0)
        {
            hasShield = true;
        }
    }


    public void HitDamage(int damage)
    {
        if (TakeDamage)
        {
            if (hasShield && shield > 0)
            {
                shield -= damage;

                if (shield == 0)
                    hasShield = false;
            }

            if (!hasShield && health > 0)
            {
                health -= damage;
                if (health == 0)
                    Destroy(this.gameObject);
            }
        }
    }
}
