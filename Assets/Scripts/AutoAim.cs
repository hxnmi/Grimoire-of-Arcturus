using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private Transform enemyPos;
    private float dist;
    public float enemyRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemyName = FindClosestEnemy();

        if (FindClosestEnemy() != null)
        {
            enemyPos = enemyName.transform;
            dist = Vector3.Distance(enemyPos.position, transform.position);
            if (dist <= enemyRange)
            {
                transform.position = enemyPos.position;
            }
            else
            {
                transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public GameObject FindClosestEnemy()
    {
        float min = 2;
        float max = 20;

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        min = min * min;
        max = max * max;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
