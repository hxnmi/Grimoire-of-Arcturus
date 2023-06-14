using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObeliskSide : Interactable
{
    [SerializeField] GameObject panel;
    void InteractObelisk()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject companion = GameObject.FindGameObjectWithTag("Companion");
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject spawner = GameObject.FindGameObjectWithTag("EnemySpawner");

        panel.SetActive(true);
        var floatingtext = gameObject.transform.GetChild(0).GetComponent<TextMesh>();
        if (floatingtext.text == "fecel")
        {
            panel.transform.GetChild(0).gameObject.SetActive(true);
            panel.transform.GetChild(1).gameObject.SetActive(false);
            panel.transform.GetChild(2).gameObject.SetActive(false);
            panel.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (floatingtext.text == "lemienne")
        {
            panel.transform.GetChild(0).gameObject.SetActive(false);
            panel.transform.GetChild(1).gameObject.SetActive(true);
            panel.transform.GetChild(2).gameObject.SetActive(false);
            panel.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (floatingtext.text == "phei")
        {
            panel.transform.GetChild(0).gameObject.SetActive(false);
            panel.transform.GetChild(1).gameObject.SetActive(false);
            panel.transform.GetChild(2).gameObject.SetActive(true);
            panel.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (floatingtext.text == "seire")
        {
            panel.transform.GetChild(0).gameObject.SetActive(false);
            panel.transform.GetChild(1).gameObject.SetActive(false);
            panel.transform.GetChild(2).gameObject.SetActive(false);
            panel.transform.GetChild(3).gameObject.SetActive(true);
        }

        foreach (GameObject go in gos)
        {
            go.GetComponent<NavMeshAgent>().enabled = false;
            go.GetComponent<EnemyController>().enabled = false;
        }
        spawner.GetComponent<SpawnEnemy>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<PlayerCombat>().enabled = false;
        companion.GetComponent<NavMeshAgent>().enabled = false;
        companion.GetComponent<CompanionController>().enabled = false;
    }

    public override string GetDescription()
    {
        return "Press [F] to interact Obelisk.";
    }

    public override void Interact()
    {
        InteractObelisk();
    }
}