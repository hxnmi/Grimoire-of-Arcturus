using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCheck : MonoBehaviour
{
    [SerializeField] Transform[] theOn;
    [SerializeField] GameObject panelMiniGame;
    GameObject player;
    GameObject companion;
    GameObject[] gos;
    GameObject spawner;
    bool stopUpdate;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        companion = GameObject.FindGameObjectWithTag("Companion");
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
    }

    void Update()
    {
        foreach (Transform child in theOn)
        {
            if (child.gameObject.activeSelf == false)
            {
                return;
            }
        }

        if (!stopUpdate)
        {
            CompleteObelisk();
            stopUpdate = true;
        }
    }

    void CompleteObelisk()
    {
        foreach (GameObject go in gos)
        {
            go.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            go.GetComponent<EnemyController>().enabled = true;
        }
        spawner.GetComponent<SpawnEnemy>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<PlayerInteraction>().Objectinteractable
            .GetComponent<MeshRenderer>()
            .materials[0]
            .EnableKeyword("_EMISSION");
        companion.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        companion.GetComponent<CompanionController>().enabled = true;
        panelMiniGame.SetActive(false);
    }

    public void closeObelisk()
    {
        foreach (GameObject go in gos)
        {
            go.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            go.GetComponent<EnemyController>().enabled = true;
        }
        spawner.GetComponent<SpawnEnemy>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        companion.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        companion.GetComponent<CompanionController>().enabled = true;
        panelMiniGame.SetActive(false);
    }
}
