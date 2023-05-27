using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHP;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            enemyHP -= 1;

            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
