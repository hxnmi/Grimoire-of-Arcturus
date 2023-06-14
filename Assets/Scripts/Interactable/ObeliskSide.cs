using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObeliskSide : Interactable
{
    [SerializeField] GameObject panel;
    void InteractObelisk()
    {
        panel.transform.GetChild(0).gameObject.GetComponent<BookSwitch>().RandomSwitch();
        panel.SetActive(true);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            go.GetComponent<NavMeshAgent>().enabled = false;
            go.GetComponent<EnemyController>().enabled = false;
        }
        GameObject spawner = GameObject.FindGameObjectWithTag("Enemy");
        spawner.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().isKinematic = true;
        GameObject companion = GameObject.FindGameObjectWithTag("Companion");
        companion.GetComponent<NavMeshAgent>().enabled = false;
        companion.GetComponent<CompanionController>().enabled = false;
    }

    public override string GetDescription()
    {
        return "Press [F] to interact Obelisk .";
    }

    public override void Interact()
    {
        InteractObelisk();
    }
}