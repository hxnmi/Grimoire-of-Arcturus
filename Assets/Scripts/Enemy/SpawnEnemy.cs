using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    private float Timer;
    [SerializeField] float maxTime;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= maxTime)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(30, 70), 1, Random.Range(30, 70));
            Instantiate(enemyPrefab, randomSpawnPos, Quaternion.identity);
            Timer = 0f;
            maxTime += 1;
        }
    }
}
