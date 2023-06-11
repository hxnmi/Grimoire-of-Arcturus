using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject companion;
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            companion.GetComponent<CompanionController>().GoTo();
        }
    }

    private void Attack()
    {
        if (EnemySensor.CurrentTargetObject)
        {
            var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
            enemy?.Damage(Random.Range(1, 10));
            //Animasi
        }
        //Animasi
    }
}
