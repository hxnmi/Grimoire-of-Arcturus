using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float Timer;
    public float maxTime;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= maxTime)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(-45, 45), 1, Random.Range(-45, 45));
            Instantiate(enemyPrefab, randomSpawnPos, Quaternion.identity);
            Timer = 0f;
            maxTime += 1;
        }
    }
}
