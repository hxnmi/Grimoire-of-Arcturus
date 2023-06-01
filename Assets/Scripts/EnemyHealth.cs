using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHP;

    public void Damage(int amount)
    {
        enemyHP -= amount;

        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        enemyHP += amount;
    }

}
