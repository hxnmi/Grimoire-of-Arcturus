using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    private float Timer;
    Vector3 randomSpawnPos;
    [SerializeField] float maxTime;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= maxTime)
        {
            randomSpawnPos = new Vector3(Random.Range(30, 120), 1, Random.Range(30, 120));
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], randomSpawnPos, Quaternion.identity);
            Timer = 0f;
            maxTime += 1;
        }
    }
}
