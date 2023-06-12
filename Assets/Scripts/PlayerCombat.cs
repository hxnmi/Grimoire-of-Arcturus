using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject companion;
    [SerializeField] GameObject EnemyAt;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            BasicAttack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HoldAttack();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            companion.GetComponent<CompanionController>().GoTo();
        }
    }

    private void BasicAttack()
    {
        if (EnemySensor.CurrentTargetObject)
        {
            var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
            enemy?.Damage(Random.Range(1, 10));
        }
        EnemyAt.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void HoldAttack()
    {
        if (EnemySensor.CurrentTargetObject)
        {
            var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
            enemy?.Damage(Random.Range(10, 30));
        }
        EnemyAt.transform.GetChild(0).gameObject.SetActive(true);
    }
}
