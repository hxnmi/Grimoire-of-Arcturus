using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAt : MonoBehaviour
{
    private int damage = 1;
    PlayerAttack player = new PlayerAttack();
    private void OnTiggerStay(Collider other)
    {
        if (other.GetComponentInParent<EnemyHealth>() != null)
        {
            if (player.Attacking)
            {
                EnemyHealth health = other.GetComponentInParent<EnemyHealth>();
                health.Damage(damage);
            }
        }
    }
}
