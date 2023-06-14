using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckAI : MonoBehaviour
{
    [SerializeField] GameObject companion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject.CompareTag("Player"))
        {
            companion.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            companion.GetComponent<CompanionController>().enabled = false;
            companion.transform.position = player.transform.position;
        }
        else if (other.gameObject.CompareTag("Plane"))
        {
            companion.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            companion.GetComponent<CompanionController>().enabled = true;
        }
    }

}
