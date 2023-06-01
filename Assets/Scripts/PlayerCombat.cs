using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (EnemySensor.CurrentTargetObject)
        {
            var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
            enemy?.Damage(Random.Range(1, 10));
        }
    }
}