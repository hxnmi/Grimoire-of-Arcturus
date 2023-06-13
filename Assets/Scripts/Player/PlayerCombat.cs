using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject companion;
    [SerializeField] GameObject enemyAt;
    [SerializeField] GameObject prefabBasicAttack;
    [SerializeField] GameObject prefabHoldAttack;

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
        GetComponent<Animation>().PlayerAttackAnimate(1);
        StartCoroutine(waitDisableKinematic(0.2f, true));
        StartCoroutine(waitDisableKinematic(1f, false));
        if (EnemySensor.CurrentTargetObject)
        {
            var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
            enemy?.Damage(Random.Range(1, 10));
            enemyAt.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            enemyAt.transform.GetChild(0).gameObject.transform.GetChild(0).SetParent(EnemySensor.CurrentTargetObject.transform);
            Instantiate(prefabBasicAttack, new Vector3(enemyAt.transform.position.x, enemyAt.transform.position.y + 1f, enemyAt.transform.position.z), Quaternion.identity, enemyAt.transform.GetChild(0).gameObject.transform);
        }
        else
            enemyAt.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void HoldAttack()
    {
        GetComponent<Animation>().PlayerAttackAnimate(2);
        StartCoroutine(waitDisableKinematic(0.2f, true));
        StartCoroutine(waitDisableKinematic(3f, false));
        if (EnemySensor.CurrentTargetObject)
        {
            var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
            enemy?.Damage(Random.Range(10, 30));
            enemyAt.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            enemyAt.transform.GetChild(1).gameObject.transform.GetChild(0).SetParent(EnemySensor.CurrentTargetObject.transform);
            Instantiate(prefabHoldAttack, new Vector3(enemyAt.transform.position.x, enemyAt.transform.position.y, enemyAt.transform.position.z), Quaternion.identity, enemyAt.transform.GetChild(1).gameObject.transform);
        }
        else
            enemyAt.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator waitDisableKinematic(float time, bool value)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Rigidbody>().isKinematic = value;
    }


}
